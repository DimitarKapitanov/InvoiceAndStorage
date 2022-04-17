namespace InvoiceAndStorage.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Buyers;
    using Microsoft.EntityFrameworkCore;

    public class BuyerService : IBuyerService
    {
        private readonly IDeletableEntityRepository<Buyer> buyerRepository;
        private readonly IDeletableEntityRepository<DatabaseОwner> dataBaseOwner;
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly ICompanyServise companyServise;

        public BuyerService(
            IDeletableEntityRepository<Buyer> buyerRepository,
            IDeletableEntityRepository<DatabaseОwner> dataBaseOwner,
            IDeletableEntityRepository<Company> companyRepository,
            ICompanyServise companyServise)
        {
            this.buyerRepository = buyerRepository;
            this.dataBaseOwner = dataBaseOwner;
            this.companyRepository = companyRepository;
            this.companyServise = companyServise;
        }

        public async Task<bool> CreateBuyer(AddBuyerViewModel model, string userId, string dataOwner)
        {
            var isCreate = false;

            var dbOwner = this.dataBaseOwner
                .All()
                .FirstOrDefault(x => x.ApplicationUsers.FirstOrDefault(i => i.Id == userId).DatabaseОwnerId == dataOwner);

            var buyer = dbOwner.Buyers.FirstOrDefault(x => x.DatabaseОwnerId == dataOwner);

            var companyId = await this.companyServise.CreateCompany(model);

            var company = await this.companyServise.GetCompany(companyId);


            if (buyer == null)
            {
                buyer = new Buyer
                {
                    CompanyId = company.Id,
                    DatabaseОwnerId = dbOwner.Id,
                };

                dbOwner.Buyers.Add(buyer);

                company.DatabaseОwnerId = dbOwner.Id;
                company.BuyerId = buyer.Id;

                this.dataBaseOwner.Update(dbOwner);
                this.companyRepository.Update(company);

                await this.buyerRepository.AddAsync(buyer);
                await this.dataBaseOwner.SaveChangesAsync();

                isCreate = true;

                return isCreate;
            }

            return isCreate;
        }

        public async Task<ICollection<BuyersViewModel>> All(string dbOwnerId)
        {
            var allBuyers = await this.buyerRepository.All()
                .Where(x => x.DatabaseОwnerId == dbOwnerId)
                .Select(x => new BuyersViewModel()
                {
                    CompanyName = x.Company.CompanyName,
                    CompanyIdentificationNumber = x.Company.IdentificationNumber,
                    CompanyOwner = x.Company.CompanyOwner,
                    CountryName = x.Company.Adress.CountryName,
                    CityName = x.Company.Adress.CityName,
                    StreetName = x.Company.Adress.StreetName,
                    StreetNumber = x.Company.Adress.StreetNumber,
                })
              .OrderBy(n => n.CompanyName)
              .ToListAsync();

            return allBuyers;
        }

        public async Task<Buyer> GetBuyer(string buyerIdentificationNumber)
        {
            var buyer = await this.buyerRepository
                .All()
                .FirstOrDefaultAsync(x => x.Company.IdentificationNumber == buyerIdentificationNumber);

            return buyer;
        }
    }
}
