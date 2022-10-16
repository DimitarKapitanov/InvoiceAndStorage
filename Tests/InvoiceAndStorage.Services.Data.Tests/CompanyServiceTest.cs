namespace InvoiceAndStorage.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Data.Repositories;
    using InvoiceAndStorage.Services.Data.Common;
    using InvoiceAndStorage.Web.ViewModels.Buyers;
    using InvoiceAndStorage.Web.ViewModels.Supplier;
    using Xunit;

    public class CompanyServiceTest
    {
        [Fact]
        public async Task CreateCompany_WihtValidData_ShouldBeCreateCompany()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adresRepository = new EfDeletableEntityRepository<Adress>(context);
            var companyService = new CompanyService(companyRepository, adresRepository);

            var companyId = await companyService.CreateCompany("Пешо ООД", "Ганчо Ганев", "111111111", "BG11AAAA12345678912345", "ДСК", "България", "Казанлък", "Раковски", 5, "ДСК");

            var findeCompany = context.Companies.FirstOrDefault(x => x.IdentificationNumber == "111111111");

            Assert.Equal(findeCompany.Id, companyId);

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

            await context.Companies.AddAsync(company);
            await context.SaveChangesAsync();

            Assert.NotNull(companyId);
            Assert.Equal("2", company.Id);

            companyId = await companyService.CreateCompany("Пешо ООД", "Ганчо Ганев", "111111111", "BG11AAAA12345678912345", "ДСК", "България", "Казанлък", "Раковски", 5, "ДСК");

            Assert.NotNull(companyId);
        }

        [Fact]
        public async Task CreateCompany_WihtAddBuyerViewModelData_ShouldBeCreateCompany()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adresRepository = new EfDeletableEntityRepository<Adress>(context);
            var companyService = new CompanyService(companyRepository, adresRepository);

            var addBuyerViewModelCompany = new AddBuyerViewModel
            {
                CompanyIdentificationNumber = "111111111",
                BankAccount = "BG11AAAA12345678912345",
                CompanyOwner = "Ганчо Ганев",
                CompanyName = "Пешо ООД",
                CityName = "Казанлък",
                CountryName = "България",
                StreetName = "Раковски",
                StreetNumber = 5,
            };

            var companyId = await companyService.CreateCompany(addBuyerViewModelCompany);

            var findCompany = context.Companies.FirstOrDefault(x => x.Id == companyId);

            Assert.Equal(findCompany.Id, companyId);

            var company = new Company()
            {
                IdentificationNumber = addBuyerViewModelCompany.CompanyIdentificationNumber,
                VatNumber = "BG" + addBuyerViewModelCompany.CompanyIdentificationNumber,
                BankAccount = addBuyerViewModelCompany.CompanyIdentificationNumber,
                CompanyOwner = addBuyerViewModelCompany.CompanyOwner,
                CompanyName = addBuyerViewModelCompany.CompanyName,
                Adress = new Adress()
                {
                    CityName = addBuyerViewModelCompany.CityName,
                    CountryName = addBuyerViewModelCompany.CountryName,
                    StreetName = addBuyerViewModelCompany.StreetName,
                    StreetNumber = addBuyerViewModelCompany.StreetNumber,
                },
            };

            await context.Companies.AddAsync(company);
            await context.SaveChangesAsync();

            Assert.NotNull(company.Id);

            companyId = await companyService.CreateCompany("Пешо ООД", "Ганчо Ганев", "111111111", "BG11AAAA12345678912345", "ДСК", "България", "Казанлък", "Раковски", 5, "ДСК");

            Assert.NotNull(companyId);
        }

        [Fact]
        public async Task CreateCompany_WihtAddSupplierViewModelData_ShouldBeCreateSupplier()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adresRepository = new EfDeletableEntityRepository<Adress>(context);
            var companyService = new CompanyService(companyRepository, adresRepository);

            var addSupplierViewModelCompany = new AddSupplierViewModel
            {
                CompanyIdentificationNumber = "111111111",
                CompanyOwner = "Ганчо Ганев",
                CompanyName = "Пешо ООД",
                CityName = "Казанлък",
                CountryName = "България",
                StreetName = "Раковски",
                StreetNumber = 5,
            };

            var companyId = await companyService.CreateCompany(addSupplierViewModelCompany);

            var findCompany = context.Companies.FirstOrDefault(x => x.Id == companyId);

            Assert.NotNull(findCompany);

            var company = new Company()
            {
                IdentificationNumber = addSupplierViewModelCompany.CompanyIdentificationNumber,
                VatNumber = "BG" + addSupplierViewModelCompany.CompanyIdentificationNumber,
                BankAccount = addSupplierViewModelCompany.CompanyIdentificationNumber,
                CompanyOwner = addSupplierViewModelCompany.CompanyOwner,
                CompanyName = addSupplierViewModelCompany.CompanyName,
                Adress = new Adress()
                {
                    CityName = addSupplierViewModelCompany.CityName,
                    CountryName = addSupplierViewModelCompany.CountryName,
                    StreetName = addSupplierViewModelCompany.StreetName,
                    StreetNumber = addSupplierViewModelCompany.StreetNumber,
                },
            };

            await context.Companies.AddAsync(company);
            await context.SaveChangesAsync();

            Assert.NotNull(companyId);

            companyId = await companyService.CreateCompany("Пешо ООД", "Ганчо Ганев", "111111111", "BG11AAAA12345678912345", "ДСК", "България", "Казанлък", "Раковски", 5, "ДСК");

            Assert.NotNull(companyId);
            Assert.Equal(findCompany.Id, companyId);
        }

        [Fact]
        public async Task GetCompany_ById_ShouldBeReturnCorectCompany()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adresRepository = new EfDeletableEntityRepository<Adress>(context);
            var companyService = new CompanyService(companyRepository, adresRepository);

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

            await context.Companies.AddAsync(company);
            await context.SaveChangesAsync();

            var companyById = await companyService.GetCompany(company.Id);

            Assert.NotNull(companyById);
        }

        [Fact]
        public async Task CreateCompany_Adress_ShouldBeCreateAdress()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adresRepository = new EfDeletableEntityRepository<Adress>(context);
            var companyService = new CompanyService(companyRepository, adresRepository);

            var adress = new Adress()
            {
                CityName = "Казанлък",
                CountryName = "България",
                Id = "1",
                StreetName = "Раковски",
                StreetNumber = 5,
            };

            await context.Adresses.AddRangeAsync(adress);
            await context.SaveChangesAsync();

            var adressId = await companyService.CreateAdress("Казанлък", "България", "Раковски", 5);

            Assert.NotNull(adressId);
        }
    }
}
