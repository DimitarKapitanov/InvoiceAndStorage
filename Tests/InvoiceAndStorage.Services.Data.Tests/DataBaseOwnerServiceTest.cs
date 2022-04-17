namespace InvoiceAndStorage.Services.Data.Tests
{
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Data.Repositories;
    using InvoiceAndStorage.Services.Data.Common;
    using Xunit;

    public class DataBaseOwnerServiceTest
    {
        [Fact]
        public async Task AddBuyer_WihtValidData_ShouldBeAddBuyerToDbOwnerRepo()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var buyerRepository = new EfDeletableEntityRepository<Buyer>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adressRepository = new EfDeletableEntityRepository<Adress>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);

            var companyService = new CompanyService(companyRepository, adressRepository);
            var buyerService = new BuyerService(buyerRepository, dbOwnerRepository, companyRepository, companyService);
            var dataBaseOwnerService = new DataBaseOwnerService(dbOwnerRepository, companyRepository, buyerRepository);

            var dbOwner = new DatabaseОwner
            {
                Id = "1",
                CompanyId = "2",
            };

            await context.DatabaseОwners.AddAsync(dbOwner);

            var buyer = new Buyer
            {
                Id = "1",
                CompanyId = "1",
                DatabaseОwnerId = "1",
            };

            await context.AddAsync(buyer);
            await context.SaveChangesAsync();

            var dbOwnerId = await dataBaseOwnerService.AddBuyer(buyer.Id, dbOwner.Id);

            Assert.Equal("1", dbOwnerId);
        }

        [Fact]
        public async Task AddUser_ByUserAndDbOwner_ShouldBeAddUserToDatabaseOwner()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var buyerRepository = new EfDeletableEntityRepository<Buyer>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adressRepository = new EfDeletableEntityRepository<Adress>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);

            var companyService = new CompanyService(companyRepository, adressRepository);
            var buyerService = new BuyerService(buyerRepository, dbOwnerRepository, companyRepository, companyService);
            var dataBaseOwnerService = new DataBaseOwnerService(dbOwnerRepository, companyRepository, buyerRepository);

            var user = new ApplicationUser()
            {
                Id = "1",
                DatabaseОwnerId = "A1",
                FirstName = "Gosho",
                LastName = "Peshov",
            };

            await context.Users.AddAsync(user);

            var dbOwner = new DatabaseОwner()
            {
                Id = "A1",
                CompanyId = "1",
            };

            await context.DatabaseОwners.AddAsync(dbOwner);
            await context.SaveChangesAsync();

            await dataBaseOwnerService.AddUser(user, dbOwner.Id);

        }

        [Fact]
        public async Task GetDatabaseОwner_ByGivingCompanyId_ShouldBeReturnDatabaseOwnerId()
        {

            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var buyerRepository = new EfDeletableEntityRepository<Buyer>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adressRepository = new EfDeletableEntityRepository<Adress>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);

            var companyService = new CompanyService(companyRepository, adressRepository);
            var buyerService = new BuyerService(buyerRepository, dbOwnerRepository, companyRepository, companyService);
            var dataBaseOwnerService = new DataBaseOwnerService(dbOwnerRepository, companyRepository, buyerRepository);

            var user = new ApplicationUser()
            {
                Id = "1",
                DatabaseОwnerId = "A1",
                FirstName = "Gosho",
                LastName = "Peshov",
            };

            await context.Users.AddAsync(user);

            var dbOwner = new DatabaseОwner()
            {
                Id = "A1",
                CompanyId = "1",
            };

            await context.DatabaseОwners.AddAsync(dbOwner);
            await context.SaveChangesAsync();

            var findDbOwnerId = await dataBaseOwnerService.GetDatabaseОwner(user.Id);

            Assert.NotNull(findDbOwnerId);
        }

        [Fact]
        public async Task CreateDataBaseOwner_ByGivingCompanyId_ShouldBeCreateDatabaseOwner()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var buyerRepository = new EfDeletableEntityRepository<Buyer>(context);
            var companyRepository = new EfDeletableEntityRepository<Company>(context);
            var adressRepository = new EfDeletableEntityRepository<Adress>(context);
            var dbOwnerRepository = new EfDeletableEntityRepository<DatabaseОwner>(context);

            var companyService = new CompanyService(companyRepository, adressRepository);
            var buyerService = new BuyerService(buyerRepository, dbOwnerRepository, companyRepository, companyService);
            var dataBaseOwnerService = new DataBaseOwnerService(dbOwnerRepository, companyRepository, buyerRepository);

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

            var dbOwnerId = dataBaseOwnerService.CreateDataBaseOwner(company.Id);

            Assert.NotNull(dbOwnerId);
        }
    }
}
