﻿namespace InvoiceAndStorage.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Data.Repositories;
    using InvoiceAndStorage.Services.Data.Common;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Product;
    using Moq;
    using Xunit;

    public class ProductServiceTest
    {
        [Fact]
        public async Task CreateProduct_ByGivenCompanyIdentificationNumber_ShouldCreatePobuct()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var supplireRepository = new EfDeletableEntityRepository<Supplier>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);
            var adresRepository = new EfDeletableEntityRepository<Adress>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);

            var companyService = new CompanyService(companyRepository, adresRepository);
            var supplireService = new SupplireService(dbOwnerRepository, companyService, companyRepository, supplireRepository);

            var productService = new ProductService(supplireService, supplireRepository, productRepository);

            var productViewModel = new AddProductViewModel()
            {
                Amount = 5,
                Name = "Pesho",
                Price = 2.0M,
            };

            var supplier = new Supplier()
            {
                Id = "1",
                CompanyId = "2",
                DatabaseОwnerId = "2",
            };

            await context.Suppliers.AddAsync(supplier);
            await context.SaveChangesAsync();

            var company = new Company
            {
                Id = "2",
                IdentificationNumber = "111111111",
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
                SupplierId = "1",
            };

            await context.Companies.AddAsync(company);
            await context.SaveChangesAsync();

            var isCreated = await productService.CreateProduct(productViewModel, "111111111", "2");

            Assert.True(isCreated);

            var product = new Product()
            {
                Amount = productViewModel.Amount,
                DeliveryDate = DateTime.UtcNow,
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                SupplierId = supplier.Id,
            };

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            isCreated = await productService.CreateProduct(productViewModel, "111111111", "2");

            Assert.False(isCreated);
        }

        [Fact]
        public async Task CreateProduct_ByGivenSupplier_ShouldCreatePobuct()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var supplireRepository = new EfDeletableEntityRepository<Supplier>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);
            var adresRepository = new EfDeletableEntityRepository<Adress>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);

            var companyService = new CompanyService(companyRepository, adresRepository);
            var supplireService = new SupplireService(dbOwnerRepository, companyService, companyRepository, supplireRepository);

            var productService = new ProductService(supplireService, supplireRepository, productRepository);

            var productViewModel = new AddProductWithoutVatNumberViewModel()
            {
                Amount = 5,
                Name = "Pesho",
                Price = 2.0M,
                CompanyIdentificationNumber = "111111111",
            };

            var supplier = new Supplier()
            {
                Id = "1",
                CompanyId = "2",
                DatabaseОwnerId = "2",
            };

            await context.Suppliers.AddAsync(supplier);
            await context.SaveChangesAsync();

            var company = new Company
            {
                Id = "2",
                IdentificationNumber = "111111111",
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
                SupplierId = "1",
            };

            await context.Companies.AddAsync(company);
            await context.SaveChangesAsync();

            var isCreated = await productService.CreateProduct(productViewModel, "1");

            Assert.True(isCreated);

            var product = new Product()
            {
                Amount = productViewModel.Amount,
                DeliveryDate = DateTime.UtcNow,
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                SupplierId = supplier.Id,
            };

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            isCreated = await productService.CreateProduct(productViewModel, "111111111");

            Assert.False(isCreated);
        }

        [Fact]
        public async Task GetAllProducts_ByGivenDatabaseOwner_ShouldReturnAllProductToGivenDatabaseOwner()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var supplireRepository = new EfDeletableEntityRepository<Supplier>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);
            var adresRepository = new EfDeletableEntityRepository<Adress>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);

            var companyService = new CompanyService(companyRepository, adresRepository);
            var supplireService = new SupplireService(dbOwnerRepository, companyService, companyRepository, supplireRepository);

            var productService = new ProductService(supplireService, supplireRepository, productRepository);

            var productViewModel = new AddProductWithoutVatNumberViewModel()
            {
                Amount = 5,
                Name = "Pesho",
                Price = 2.0M,
                CompanyIdentificationNumber = "111111111",
            };

            var dbOwner = new DatabaseОwner()
            {
                Id = "A1",
                CompanyId = "1",
            };

            await context.DatabaseОwners.AddAsync(dbOwner);
            await context.SaveChangesAsync();

            var supplier = new Supplier()
            {
                Id = "1",
                CompanyId = "2",
                DatabaseОwnerId = "2",
            };

            await context.Suppliers.AddAsync(supplier);
            await context.SaveChangesAsync();

            var company = new Company
            {
                Id = "2",
                IdentificationNumber = "111111111",
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
                SupplierId = "1",
            };

            dbOwner.Suppliers.Add(supplier);

            context.DatabaseОwners.Update(dbOwner);

            await context.Companies.AddAsync(company);
            await context.SaveChangesAsync();

            var product = new Product()
            {
                Amount = productViewModel.Amount,
                DeliveryDate = DateTime.UtcNow,
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                SupplierId = supplier.Id,
            };

            await context.Products.AddAsync(product);
            await context.Products.AddAsync(product);
            await context.Products.AddAsync(product);

            supplier.Products.Add(product);
            supplier.Products.Add(product);
            supplier.Products.Add(product);

            context.Suppliers.Update(supplier);

            var productsModel = new ProductViewModel()
            {
                Amount = product.Amount,
                DeliveryDate = DateTime.UtcNow,
                Name = product.Name,
                Price = product.Price,
                SupplierName = supplier.Company.CompanyName,
            };

            await context.SaveChangesAsync();

            var listOfProducts = new List<ProductViewModel>();

            listOfProducts.Add(productsModel);
            listOfProducts.Add(productsModel);
            listOfProducts.Add(productsModel);

            var allproducts = await productService.GetAllProducts(dbOwner);

            Assert.NotEmpty(allproducts);
            Assert.NotNull(allproducts);
        }

        [Fact]
        public async Task GetProductByName_ByGivenNameAndQuantity_ShouldReturnProduct()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var supplireRepository = new EfDeletableEntityRepository<Supplier>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);
            var adresRepository = new EfDeletableEntityRepository<Adress>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);

            var companyService = new CompanyService(companyRepository, adresRepository);
            var supplireService = new SupplireService(dbOwnerRepository, companyService, companyRepository, supplireRepository);

            var productService = new ProductService(supplireService, supplireRepository, productRepository);

            var product = new Product()
            {
                Amount = 5,
                DeliveryDate = DateTime.UtcNow,
                Name = "Banan",
                Price = 2.5M,
                SupplierId = "5",
            };

            await context.Products.AddAsync(product);

            await context.SaveChangesAsync();

            var productByName = await productService.GetProductByName("Banan", 5);

            Assert.NotNull(productByName);

            productByName = await productService.GetProductByName("Banana", 5);

            Assert.Null(productByName);
        }

        [Fact]
        public async Task CreateProduct_ReturnsTrue()
        {
            // Arrange
            var addProductViewModel = new AddProductWithoutVatNumberViewModel
            {
                Amount = 1,
                CompanyIdentificationNumber = "123456789",
                Name = "Product 1",
                Price = 10,
            };
            var supplierId = "supplier-id";
            var supplierServiceMock = new Mock<ISupplierService>();
            var productRepositoryMock = new Mock<IDeletableEntityRepository<Product>>();
            var supplierRepositoryMock = new Mock<IDeletableEntityRepository<Supplier>>();
            var supplier = new Supplier { IdentificationNumber = "123456789", Products = new List<Product>() };
            supplierServiceMock.Setup(x => x.GetSupplierByIdentificationNumber(supplierId, addProductViewModel.CompanyIdentificationNumber)).ReturnsAsync(supplier);
            productRepositoryMock.Setup(x => x.All()).Returns(new List<Product>().AsQueryable());
            var sut = new ProductService((ISupplierService)productRepositoryMock.Object, (IDeletableEntityRepository<Supplier>)supplierServiceMock.Object, (IDeletableEntityRepository<Product>)supplierRepositoryMock.Object);

            // Act
            var result = await sut.CreateProduct(addProductViewModel, supplierId);

            // Assert
            Assert.False(result);
        }
    }
}
