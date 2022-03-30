namespace InvoiceAndStorage.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Buyers;

    public class BuyerService : IBuyerService
    {
        private readonly IDeletableEntityRepository<Buyer> buyerRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> applicationUser;
        private readonly IDeletableEntityRepository<DatabaseОwner> dataBaseOwner;
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly ICompanyServise companyServise;
        private readonly IDataBaseOwnerService dataBaseOwnerService;

        public BuyerService(
            IDeletableEntityRepository<Buyer> buyerRepository,
            IDeletableEntityRepository<ApplicationUser> applicationUser,
            IDeletableEntityRepository<DatabaseОwner> dataBaseOwner,
            IDeletableEntityRepository<Company> companyRepository,
            ICompanyServise companyServise,
            IDataBaseOwnerService dataBaseOwnerService)
        {
            this.buyerRepository = buyerRepository;
            this.applicationUser = applicationUser;
            this.dataBaseOwner = dataBaseOwner;
            this.companyRepository = companyRepository;
            this.companyServise = companyServise;
            this.dataBaseOwnerService = dataBaseOwnerService;
        }

        public async Task<bool> CreateBuyer(AddBuyerViewModel model, string userId)
        {
            var user = this.applicationUser.All().FirstOrDefault(x => x.Id == userId);

            var databaseOwner = user.DatabaseОwner;

            var buyer = databaseOwner.Buyers.FirstOrDefault(x => x.DatabaseОwnerId == databaseOwner.Id);

            var isCreate = false;

            if (buyer == null)
            {
                buyer = new Buyer
                {
                    CompanyId = await this.companyServise.CreateCompany(model),
                };

                return isCreate;
            }

            return isCreate;
        }

        public async Task<ICollection<BuyersViewModel>> All(string dbOwnerId)
        {
            var dbOwner = this.dataBaseOwner.All().FirstOrDefault(o => o.Id == dbOwnerId);

            var allBuyers = dbOwner.Buyers.Where(x => x.Id == x.Company.BuyerId)
                  .Select(x => new BuyersViewModel()
                  {
                      CompanyName = x.Company.CompanyName,
                      CompanyIdentificationNumber = x.Company.IdentificationNumber,
                      CompanyOwner = x.Company.CompanyOwner,
                      CountryName = x.Company.Adress.Country.CountryName,
                      CityName = x.Company.Adress.City.CityName,
                      StreetName = x.Company.Adress.Street.StreetName,
                      StreetNumber = x.Company.Adress.Street.StreetNumber,
                  })
              .OrderBy(n => n.CompanyName)
              .ToList();

            return default;
        }
    }
}
