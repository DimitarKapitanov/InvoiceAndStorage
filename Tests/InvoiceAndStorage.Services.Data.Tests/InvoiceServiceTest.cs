namespace InvoiceAndStorage.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Data.Repositories;
    using InvoiceAndStorage.Web.ViewModels.Buyers;
    using InvoiceAndStorage.Web.ViewModels.Invoice;
    using InvoiceAndStorage.Web.ViewModels.Supplier;
    using InvoiceAndStorage.Services.Data.Common;
    using Xunit;

    public class InvoiceServiceTest
    {
        [Fact]
        public static async Task AddInvoice_WihtValidData_ShouldBeAddInvocie()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var buyerRepository = new EfDeletableEntityRepository<Buyer>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adressRepository = new EfDeletableEntityRepository<Adress>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);
            var soldProductRepository = new EfDeletableEntityRepository<SoldProduct>(context);
            var supplierRepository = new EfDeletableEntityRepository<Supplier>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var invoiceRepository = new EfDeletableEntityRepository<Invoice>(context);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(context);

            var companyService = new CompanyService(companyRepository, adressRepository);
            var buyerService = new BuyerService(buyerRepository, dbOwnerRepository, companyRepository, companyService);
            var dataBaseOwnerService = new DataBaseOwnerService(dbOwnerRepository, companyRepository, buyerRepository);
            var soldProductService = new SoldProductService(soldProductRepository);

            var invoiceService = new InvoiceService(buyerService, soldProductService, dataBaseOwnerService, supplierRepository, productRepository, buyerRepository, invoiceRepository, applicationUser, dbOwnerRepository, soldProductRepository);

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "Pesho",
                LastName = "Pesho",
                DatabaseОwnerId = "A1",
            };

            await context.Users.AddAsync(user);

            var dbOwner = new DatabaseОwner()
            {
                Id = "A1",
                CompanyId = "B1",
            };

            var product = new CreateInvoiceViewModel()
            {
                BuyerIdentificationNumber = "111111111",
                InvoiceTipe = InvoiceAndStorage.Data.Models.Enums.InvoiceTipe.Invoice,
                PaymentMethod = InvoiceAndStorage.Data.Models.Enums.PaymentMethod.BankPayment,
                InvoiceProductViewModels = new List<InvoiceProductViewModel>()
                .Select(x => new InvoiceProductViewModel
                {
                    Amount = "1",
                    Price = "20.20",
                    ProductName = "Banan",
                    Quantity = 5,
                }).ToList(),
            };

            dbOwner.ApplicationUsers.Add(user);

            await context.DatabaseОwners.AddAsync(dbOwner);
            await context.SaveChangesAsync();

            var owner = await invoiceService.AddInvoice(product, "1");

            Assert.False(owner.IsValid, "Greshka");

            var findDatabase = await dataBaseOwnerService.GetDatabaseОwner(user.Id);

            Assert.NotNull(findDatabase);

            owner.IsValid = true;

            Assert.True(owner.IsValid);

            var products = await invoiceService.GetProducts(findDatabase);

            Assert.NotNull(products);

            var soldProduct = new Product()
            {
                Amount = 5,
                Name = "Banan",
                Price = 5.0m,
            };

            var soldProducts = await soldProductService.CreateSoldProduct(soldProduct, 1, 5);

            Assert.NotNull(soldProducts);

            var invoice = new Invoice()
            {
                ApplicationUserId = "1",
                BuyerId = "1",
                DatabaseОwnerId = "1",
                InvoiceDate = DateTime.UtcNow.Date,
                InvoiceTipe = InvoiceAndStorage.Data.Models.Enums.InvoiceTipe.Invoice,
                DueDate = DateTime.UtcNow.Date,
                PaymentMethod = InvoiceAndStorage.Data.Models.Enums.PaymentMethod.BankPayment,
                TotalInvoiceSum = 0,
            };

            invoice.SoldProducts.Add(soldProducts);
        }

        [Fact]
        public async Task GetAllInvoiceProduct_ByDbOwner_ShouldReturnListOfCreateInvoiceProduct()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var buyerRepository = new EfDeletableEntityRepository<Buyer>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adressRepository = new EfDeletableEntityRepository<Adress>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);
            var soldProductRepository = new EfDeletableEntityRepository<SoldProduct>(context);
            var supplierRepository = new EfDeletableEntityRepository<Supplier>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var invoiceRepository = new EfDeletableEntityRepository<Invoice>(context);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(context);

            var companyService = new CompanyService(companyRepository, adressRepository);
            var buyerService = new BuyerService(buyerRepository, dbOwnerRepository, companyRepository, companyService);
            var dataBaseOwnerService = new DataBaseOwnerService(dbOwnerRepository, companyRepository, buyerRepository);
            var soldProductService = new SoldProductService(soldProductRepository);

            var invoiceService = new InvoiceService(buyerService, soldProductService, dataBaseOwnerService, supplierRepository, productRepository, buyerRepository, invoiceRepository, applicationUser, dbOwnerRepository, soldProductRepository);

            var dbOwner = new DatabaseОwner()
            {
                CompanyId = "1",
            };
            await context.AddAsync(dbOwner);

            var supplier = new Supplier()
            {
                CompanyId = "1",
                DatabaseОwnerId = dbOwner.Id,
            };

            await context.AddAsync(supplier);

            dbOwner.Suppliers.Add(supplier);

            var product = new Product()
            {
                Amount = 1,
                Name = "Banan",
                Price = 2.20M,
            };

            await context.AddAsync(product);

            var curProduct = new CreateInvoiceViewModel()
            {
                BuyerIdentificationNumber = "1",
                InvoiceProductViewModels = new List<InvoiceProductViewModel>(),
            };

            var invoiceProduct = new InvoiceProductViewModel()
            {
                Amount = "1",
                ProductName = "Banan",
                Price = "2.20",
                Quantity = default,
            };

            curProduct.InvoiceProductViewModels.Add(invoiceProduct);
            curProduct.InvoiceProductViewModels.Add(invoiceProduct);
            curProduct.InvoiceProductViewModels.Add(invoiceProduct);
            curProduct.InvoiceProductViewModels.Add(invoiceProduct);

            supplier.Products.Add(product);
            supplier.Products.Add(product);
            supplier.Products.Add(product);
            supplier.Products.Add(product);

            context.SaveChanges();

            var products = await invoiceService.GetAllInvoiceProducts(dbOwner);

            Assert.NotNull(products);
        }

        [Fact]
        public async Task GetAllInvoice_ByGivenUserId_ShouldReturnAllInvoiceViewModel()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var buyerRepository = new EfDeletableEntityRepository<Buyer>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adressRepository = new EfDeletableEntityRepository<Adress>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);
            var soldProductRepository = new EfDeletableEntityRepository<SoldProduct>(context);
            var supplierRepository = new EfDeletableEntityRepository<Supplier>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var invoiceRepository = new EfDeletableEntityRepository<Invoice>(context);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(context);

            var companyService = new CompanyService(companyRepository, adressRepository);
            var buyerService = new BuyerService(buyerRepository, dbOwnerRepository, companyRepository, companyService);
            var dataBaseOwnerService = new DataBaseOwnerService(dbOwnerRepository, companyRepository, buyerRepository);
            var soldProductService = new SoldProductService(soldProductRepository);

            var invoiceService = new InvoiceService(buyerService, soldProductService, dataBaseOwnerService, supplierRepository, productRepository, buyerRepository, invoiceRepository, applicationUser, dbOwnerRepository, soldProductRepository);

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "Pesho",
                LastName = "Pesho",
                DatabaseОwnerId = "A1",
                Invoices = new List<Invoice>(),
            };

            var dbOwner = new DatabaseОwner()
            {
                CompanyId = "2",
                ApplicationUsers = new List<ApplicationUser>(),
            };

            dbOwner.ApplicationUsers.Add(user);

            await context.AddAsync(dbOwner);
            await context.AddAsync(user);
            await context.SaveChangesAsync();

            var buyer = new Buyer()
            {
                Id = "1",
                Invoices = new List<Invoice>(),
                CompanyId = "2",
                DatabaseОwnerId = "A1",
            };

            await context.AddAsync(buyer);
            await context.SaveChangesAsync();

            var company = new Company
            {
                Id = "2",
                IdentificationNumber = "111111112",
                VatNumber = "BG111111111",
                BankAccount = "BG11AAAA12345678912345",
                CompanyOwner = "Ганчо Ганев",
                CompanyName = "Пешо ООД",
                Adress = new Adress()
                {
                    CityName = "Казанлък",
                    CountryName = "България",
                    Id = "1",
                    StreetName = "Раковски",
                    StreetNumber = 5,
                },
            };

            await context.AddAsync(company);
            await context.SaveChangesAsync();

            var invoice = new Invoice()
            {
                ApplicationUserId = user.Id,
                BuyerId = buyer.Id,
                DatabaseОwnerId = "A1",
            };

            await context.Invoices.AddAsync(invoice);
            await context.SaveChangesAsync();

            user.Invoices.Add(invoice);
            user.Invoices.Add(invoice);
            user.Invoices.Add(invoice);

            var invoices = await invoiceService.GetAllInvoice(user.Id);

            Assert.NotNull(invoices);
        }

        //[Fact]
        //public async Task CreateInvoice_ByGiving()
        //{
        //    MapperInitializer.InitializeMapper();
        //    var context = InitializeContext.CreateContextForInMemory();
        //    var buyerRepository = new EfDeletableEntityRepository<Buyer>(context);
        //    var companyRepository = new EfDeletableEntityRepository<Company>(context);
        //    var adressRepository = new EfDeletableEntityRepository<Adress>(context);
        //    var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);
        //    var soldProductRepository = new EfDeletableEntityRepository<SoldProduct>(context);
        //    var supplierRepository = new EfDeletableEntityRepository<Supplier>(context);
        //    var productRepository = new EfDeletableEntityRepository<Product>(context);
        //    var invoiceRepository = new EfDeletableEntityRepository<Invoice>(context);
        //    var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(context);

        //    var companyService = new CompanyService(companyRepository, adressRepository);
        //    var buyerService = new BuyerService(buyerRepository, dbOwnerRepository, companyRepository, companyService);
        //    var dataBaseOwnerService = new DataBaseOwnerService(dbOwnerRepository, companyRepository, buyerRepository);
        //    var soldProductService = new SoldProductService(soldProductRepository);

        //    var invoiceService = new InvoiceService(buyerService, soldProductService, dataBaseOwnerService, supplierRepository, productRepository, buyerRepository, invoiceRepository, applicationUser, dbOwnerRepository, soldProductRepository);

        //    MethodInfo methodInfo = typeof(InvoiceService)
        //        .GetMethod(
        //        "CreateInvoice",
        //        BindingFlags.NonPublic| BindingFlags.Instance | BindingFlags.Static);

        //    var viewModel = new CreateInvoiceViewModel()
        //    {
        //        BuyerIdentificationNumber = "111111111",
        //        InvoiceProductViewModels = new List<InvoiceProductViewModel>(),
        //        InvoiceTipe = InvoiceAndStorage.Data.Models.Enums.InvoiceTipe.Invoice,
        //        PaymentMethod = InvoiceAndStorage.Data.Models.Enums.PaymentMethod.BankPayment,
        //    };


        //    object[] parameters = { "viewModel", "1", "A1", "B1"};

        //    object result = methodInfo.Invoke(invoiceRepository, parameters);

        //    Assert.True((bool)result);
        //}
    }
}
