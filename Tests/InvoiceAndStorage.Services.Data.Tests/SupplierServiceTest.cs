using InvoiceAndStorage.Data.Models;
using InvoiceAndStorage.Data.Repositories;
using InvoiceAndStorage.Services.Data.Common;
using InvoiceAndStorage.Web.ViewModels.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceAndStorage.Services.Data.Tests
{
    public class SupplierServiceTest
    {
        [Fact]
        public async Task AllSuppliers_ByGivingDbOwnerId_ShouldReturnColectionOfSuppliers()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var supplireRepository = new EfDeletableEntityRepository<Supplier>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);
            var adresRepository = new EfDeletableEntityRepository<Adress>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);

            var companyService = new CompanyService(companyRepository, adresRepository);
            var supplireService = new SupplireService(dbOwnerRepository, companyService, companyRepository, supplireRepository);


            var dbOwner = new DatabaseОwner()
            {
                Id = "A1",
                CompanyId = "1",
                Suppliers = new List<Supplier>(),
            };

            var supplier = new Supplier()
            {
                Id = "1",
                CompanyId = "2",
                DatabaseОwnerId = "2",
            };

            await context.AddAsync(supplier);
            await context.AddAsync(dbOwner);

            await context.SaveChangesAsync();

            dbOwner.Suppliers.Add(supplier);
            dbOwner.Suppliers.Add(supplier);
            dbOwner.Suppliers.Add(supplier);
            dbOwner.Suppliers.Add(supplier);

            var allSupplier = await supplireService.AllSuppliers(dbOwner.Id);

            Assert.NotNull(allSupplier);
        }

        [Fact]
        public async Task CreateSupplire_ByGivingUserIdDbOwnerIdAndViewModel_ShouldReturnTrueIfSupplierIsCreated()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var supplireRepository = new EfDeletableEntityRepository<Supplier>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);
            var adresRepository = new EfDeletableEntityRepository<Adress>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);

            var companyService = new CompanyService(companyRepository, adresRepository);
            var supplireService = new SupplireService(dbOwnerRepository, companyService, companyRepository, supplireRepository);


            var dbOwner = new DatabaseОwner()
            {
                Id = "2",
                CompanyId = "1",
                Suppliers = new List<Supplier>(),
            };

            var user = new ApplicationUser()
            {
                DatabaseОwnerId = "2",
                FirstName = "Pesho",
                LastName = "Pesho",
                Id = "1",
            };

            await context.AddAsync(user);
            await context.AddAsync(dbOwner);

            var viewModel = new AddSupplierViewModel()
            {
                CompanyIdentificationNumber = "111111112",
                CityName = "Казанлък",
                CompanyName = "Пешо ООД",
                CompanyOwner = "Ганчо Ганев",
                CountryName = "България",
                StreetName = "Раковски",
                StreetNumber = 5,
            };

            await context.SaveChangesAsync();


            var isCreated = await supplireService.CreateSupplire(viewModel, "1", dbOwner.Id);

            Assert.True(isCreated);

            isCreated = await supplireService.CreateSupplire(viewModel, "1", dbOwner.Id);

            Assert.False(isCreated);
        }
    }
}
