namespace InvoiceAndStorage.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Data.Repositories;
    using InvoiceAndStorage.Services.Data.Common;
    using InvoiceAndStorage.Web.ViewModels.Buyers;
    using Xunit;

    public class BuyerServiceTest
    {
        [Fact]
        public async Task CreateBuyer_WihtValidData_ShouldBeCreateBuyer()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var buyerRepository = new EfDeletableEntityRepository<Buyer>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adressRepository = new EfDeletableEntityRepository<Adress>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);

            var companyService = new CompanyService(companyRepository, adressRepository);
            var buyerService = new BuyerService(buyerRepository, dbOwnerRepository, companyRepository, companyService);

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "Гошо",
                LastName = "Goshev",
                DatabaseОwnerId = "A1",
            };

            await context.AddAsync(user);
            await context.SaveChangesAsync();

            var dbOwner = new DatabaseОwner()
            {
                Id = "A1",
                CompanyId = "1",
            };

            await context.AddAsync(dbOwner);
            await context.SaveChangesAsync();

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

            var isCreated = await buyerService.CreateBuyer(addBuyerViewModelCompany, user.Id, dbOwner.Id);

            Assert.True(isCreated);

            var buyer = new Buyer()
            {
                Id = "1",
                CompanyId = company.Id,
                DatabaseОwnerId = dbOwner.Id,
            };

            dbOwner.Buyers.Add(buyer);
            context.DatabaseОwners.Update(dbOwner);

            var findBuyer = dbOwner.Buyers.FirstOrDefault(x => x.DatabaseОwnerId == "2");

            Assert.Null(findBuyer);

            isCreated = await buyerService.CreateBuyer(addBuyerViewModelCompany, user.Id, dbOwner.Id);

            Assert.False(isCreated);
        }

        [Fact]
        public async Task GetBuyer_ByCompanyIdentificationNumber_ShouldBeReturnBuyer()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var buyerRepository = new EfDeletableEntityRepository<Buyer>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adressRepository = new EfDeletableEntityRepository<Adress>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);

            var companyService = new CompanyService(companyRepository, adressRepository);
            var buyerService = new BuyerService(buyerRepository, dbOwnerRepository, companyRepository, companyService);

            var addBuyerViewModelCompany = new AddBuyerViewModel
            {
                CompanyIdentificationNumber = "111111111",
                CompanyOwner = "Ганчо Ганев",
                CompanyName = "Пешо ООД",
                CityName = "Казанлък",
                CountryName = "България",
                StreetName = "Раковски",
                StreetNumber = 5,
            };

            var dbOwner = new DatabaseОwner()
            {
                Id = "A1",
                CompanyId = "1",
            };

            await context.DatabaseОwners.AddAsync(dbOwner);
            await context.SaveChangesAsync();

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

            var buyer = new Buyer()
            {
                CompanyId = company.Id,
                DatabaseОwnerId = dbOwner.Id,
            };
            await context.Buyers.AddAsync(buyer);
            await context.SaveChangesAsync();

            company.BuyerId = buyer.Id;
            dbOwner.Buyers.Add(buyer);

            context.Companies.Update(company);

            await context.SaveChangesAsync();

            var returnedBuyer = await buyerService.GetBuyer(addBuyerViewModelCompany.CompanyIdentificationNumber);

            var findBuyer = buyerRepository.All().FirstOrDefault(x => x.Id == returnedBuyer.Id);
            var findCompany = companyRepository.All().FirstOrDefault(x => x.Id == company.Id);

            Assert.NotNull(findBuyer);

            Assert.NotNull(findCompany);

            Assert.Equal(findCompany.Id, findBuyer.CompanyId);
        }

        [Fact]
        public async Task GetAllBuyers_ByDbOwnerId_ShouldBeReturnCorectBuyer()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var buyerRepository = new EfDeletableEntityRepository<Buyer>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adressRepository = new EfDeletableEntityRepository<Adress>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);

            var companyService = new CompanyService(companyRepository, adressRepository);
            var buyerService = new BuyerService(buyerRepository, dbOwnerRepository, companyRepository, companyService);

            var dbOwner = new DatabaseОwner()
            {
                Id = "A1",
                CompanyId = "1",
            };
            await context.AddAsync(dbOwner);
            await context.SaveChangesAsync();

            var findBuyer = await buyerService.All(dbOwner.Id);

            Assert.NotNull(findBuyer);
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
