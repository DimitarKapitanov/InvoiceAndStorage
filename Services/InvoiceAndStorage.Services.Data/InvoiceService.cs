namespace InvoiceAndStorage.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Invoice;
    using Microsoft.EntityFrameworkCore;

    public class InvoiceService : IInvoiceService
    {
        private readonly IBuyerService buyerService;
        private readonly ISoldProductService soldProductService;
        private readonly IDataBaseOwnerService dataBaseOwnerService;
        private readonly IDeletableEntityRepository<Buyer> buyerRepository;
        private readonly IDeletableEntityRepository<Invoice> invoiceRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<DatabaseОwner> dbOwnerRepository;
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<Supplier> supplierRepository;

        public InvoiceService(
            IBuyerService buyerService,
            ISoldProductService soldProductService,
            IDataBaseOwnerService dataBaseOwnerService,
            IDeletableEntityRepository<Supplier> supplierRepository,
            IDeletableEntityRepository<Product> productRepository,
            IDeletableEntityRepository<Buyer> buyerRepository,
            IDeletableEntityRepository<Invoice> invoiceRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<DatabaseОwner> dbOwnerRepository)
        {
            this.supplierRepository = supplierRepository;
            this.buyerService = buyerService;
            this.soldProductService = soldProductService;
            this.dataBaseOwnerService = dataBaseOwnerService;
            this.productRepository = productRepository;
            this.buyerRepository = buyerRepository;
            this.invoiceRepository = invoiceRepository;
            this.userRepository = userRepository;
            this.dbOwnerRepository = dbOwnerRepository;
        }

        public async Task<(bool IsValid, string Error)> AddInvoice(CreateInvoiceViewModel product, string userId)
        {
            var isCreate = false;
            var error = string.Empty;

            var dataOwnerId = await this.dataBaseOwnerService.GetDatabaseОwner(userId);

            if (dataOwnerId == null)
            {
                error = $"Не е намерен собственик на фирмата с това ЕИК: {product.BuyerIdentificationNumber}";

                return (isCreate, error);
            }

            var productForUpdate = await this.GetProducts(dataOwnerId);

            var buyer = await this.buyerService.GetBuyer(product.BuyerIdentificationNumber);

            if (buyer == null)
            {
                error = $"Не е намерен куповач с това ЕИК: {buyer.Id}!";

                return (isCreate, error);
            }

            var currInvoice = await CreateInvoice(product, userId, buyer.Id, dataOwnerId);

            await this.invoiceRepository.AddAsync(currInvoice);
            await this.invoiceRepository.SaveChangesAsync();

            var updatedProduct = new List<Product>();

            foreach (var products in productForUpdate)
            {
                foreach (var modelProduct in product.InvoiceProductViewModels.Where(x => x.Quantity > 0))
                {
                    if (products.Name != modelProduct.ProductName)
                    {
                        continue;
                    }

                    if (modelProduct.Quantity > products.Amount)
                    {
                        error = $"Желаното количество не може да надвишава текущото. Променете желаното количество на {modelProduct.ProductName}";

                        return (isCreate, error);
                    }
                    else
                    {
                        var soldProduct = await this.soldProductService.CreateSoldProduct(modelProduct, currInvoice.Id);

                        currInvoice.TotalInvoiceSum += modelProduct.Price * modelProduct.Quantity;
                        this.invoiceRepository.Update(currInvoice);

                        products.Amount -= modelProduct.Quantity;
                        this.productRepository.Update(products);

                        updatedProduct.Add(products);
                    }
                }
            }

            currInvoice.TotalInvoiceSum *= 1.2M;

            isCreate = true;

            foreach (var item in updatedProduct)
            {
                buyer.Product.Add(item);
                this.buyerRepository.Update(buyer);
            }

            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.Id == userId);

            user.Invoices.Add(currInvoice);

            await this.invoiceRepository.SaveChangesAsync();
            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();
            await this.buyerRepository.SaveChangesAsync();
            await this.productRepository.SaveChangesAsync();

            var invoice = this.invoiceRepository.All().Include(x => x.SoldProducts).Where(x => x.Id == x.SoldProducts.FirstOrDefault(y => y.InvoiceId == x.Id).InvoiceId).ToList();

            return (isCreate, error);
        }

        private static Task<Invoice> CreateInvoice(CreateInvoiceViewModel product, string userId, string buyerId, string dataOwnerId)
        {
            var invoice = new Invoice()
            {
                ApplicationUserId = userId,
                BuyerId = buyerId,
                DatabaseОwnerId = dataOwnerId,
                InvoiceTipe = product.InvoiceTipe,
                PaymentMethod = product.PaymentMethod,
                DueDate = DateTime.UtcNow.Date,
                InvoiceDate = DateTime.UtcNow.Date,
                TotalInvoiceSum = default,
            };

            return Task.FromResult(invoice);
        }

        public async Task<CreateInvoiceViewModel> GetAllInvoiceProducts(DatabaseОwner dbOwner)
        {
            var productsBySupplier = await this.supplierRepository.All()
                .Where(x => x.DatabaseОwnerId == dbOwner.Id)
                .Select(p => p.Products
                    .Select(a => new
                    {
                        a.Name,
                        a.Amount,
                        a.Price,
                    }).ToList()).ToListAsync();

            var products = new CreateInvoiceViewModel
            {
                InvoiceProductViewModels = new List<InvoiceProductViewModel>(),
            };

            foreach (var currProducts in productsBySupplier)
            {
                foreach (var item in currProducts)
                {
                    var productViewModel = new InvoiceProductViewModel
                    {
                        ProductName = item.Name,
                        Amount = item.Amount,
                        Price = item.Price,
                        Quantity = default,
                    };

                    products.InvoiceProductViewModels.Add(productViewModel);
                }
            }

            return products;
        }

        public async Task<ICollection<Product>> GetProducts(string dataOwnerId)
        {
            var products = await this.productRepository
                .All()
                .Include(x => x.Supplier)
                .Where(x => x.Supplier.DatabaseОwnerId == dataOwnerId)
                .ToListAsync();

            return products;
        }

        public async Task<AllInvoiceViewModel> GetAllInvoice(string userId)
        {
            var dbOwnerId = await this.dataBaseOwnerService.GetDatabaseОwner(userId);

            var allInvoice = new AllInvoiceViewModel();

            var invoices = this.invoiceRepository
                .All()
                .Include(b => b.Buyer)
                .ThenInclude(c => c.Company)
                .Include(u => u.ApplicationUser)
                .ToList()
                .Select(x => new InvoiceViewModel()
                {
                    BuyerName = x.Buyer.Company.CompanyName,
                    InvoiceDate = x.InvoiceDate,
                    InvoiceNumber = x.Id,
                    UserName = x.ApplicationUser.FirstName+ " " + x.ApplicationUser.LastName,
                    TotalInvoiceSum = x.TotalInvoiceSum,
                }).ToList();

            foreach (var item in invoices)
            {
                allInvoice.InvoiceViewModels.Add(item);
            }

            return allInvoice;
        }
    }
}
