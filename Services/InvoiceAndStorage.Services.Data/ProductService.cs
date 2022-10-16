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
    using InvoiceAndStorage.Web.ViewModels.Product;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<Supplier> supplierRepository;
        private readonly ISupplierSevice supplierSevice;

        public ProductService(
            ISupplierSevice supplierSevice,
            IDeletableEntityRepository<Supplier> supplierRepository,
            IDeletableEntityRepository<Product> productRepository)
        {
            this.supplierSevice = supplierSevice;
            this.supplierRepository = supplierRepository;
            this.productRepository = productRepository;
        }

        public async Task<bool> CreateProduct(AddProductViewModel addProductVewModel, string companyIdentificationNumber, string ownerId)
        {
            var supplier = await this.supplierSevice.GetSupplierByIdentificationNumber(companyIdentificationNumber, ownerId);

            var isValidProduct = await this.productRepository.All().AnyAsync(x => x.Name == addProductVewModel.Name);

            var isCreated = false;

            if (isValidProduct)
            {
                return isCreated;
            }

            var product = new Product
            {
                Amount = addProductVewModel.Amount,
                DeliveryDate = DateTime.UtcNow,
                Name = addProductVewModel.Name,
                Price = addProductVewModel.Price,
                SupplierId = supplier.Id,
            };

            supplier.Products.Add(product);

            await this.productRepository.AddAsync(product);
            this.supplierRepository.Update(supplier);
            await this.productRepository.SaveChangesAsync();

            isCreated = true;

            return isCreated;
        }

        public async Task<bool> CreateProduct(AddProductWithoutVatNumberViewModel addProductVewModel, string supplierId)
        {
            var isCreated = false;

            var supplier = await this.supplierSevice.GetSupplierByIdentificationNumber(supplierId, addProductVewModel.CompanyIdentificationNumber);

            var isValidProduct = await this.productRepository.All().AnyAsync(x => x.Name == addProductVewModel.Name);

            if (!isValidProduct)
            {
                return isCreated;
            }

            var product = new Product
            {
                Amount = addProductVewModel.Amount,
                DeliveryDate = DateTime.UtcNow,
                Name = addProductVewModel.Name,
                Price = addProductVewModel.Price,
                SupplierId = supplierId,
            };

            supplier.Products.Add(product);

            await this.productRepository.AddAsync(product);
            this.supplierRepository.Update(supplier);
            await this.productRepository.SaveChangesAsync();

            isCreated = true;

            return isCreated;
        }

        public async Task<ICollection<ProductViewModel>> GetAllProducts(DatabaseОwner dbOwner)
        {
            var productsBySupplier = await this.supplierRepository.All()
                .Where(x => x.DatabaseОwnerId == dbOwner.Id)
                .Select(p => p.Products
                    .Select(a => new
                    {
                        a.Name,
                        a.Amount,
                        a.Price,
                        SupplierName = p.Company.CompanyName,
                        DeliveryDate = DateTime.UtcNow.Date,
                    }).ToList()).ToListAsync();

            var allProducts = new List<ProductViewModel>();

            foreach (var products in productsBySupplier)
            {
                foreach (var item in products)
                {
                    var productViewModel = new ProductViewModel
                    {
                        Name = item.Name,
                        Amount = item.Amount,
                        Price = item.Price,
                        SupplierName = item.SupplierName,
                        DeliveryDate = item.DeliveryDate.Date,
                    };

                    allProducts.Add(productViewModel);
                }
            }

            return allProducts;
        }

        public async Task<InvoiceProductViewModel> GetProductByName(string productName, int quantity)
        {
            var product = new InvoiceProductViewModel();

            var dbProduct = await this.productRepository.All().FirstOrDefaultAsync(x => x.Name == productName);

            if (dbProduct == null)
            {
                return null;
            }

            product.Amount = dbProduct.Amount.ToString();
            product.Price = dbProduct.Price.ToString();
            product.ProductName = dbProduct.Name;
            product.Quantity = quantity;

            return product;
        }
    }
}
