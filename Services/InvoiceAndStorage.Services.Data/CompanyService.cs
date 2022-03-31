namespace InvoiceAndStorage.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Buyers;

    public class CompanyService : ICompanyServise
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        private readonly IDeletableEntityRepository<Company> companyRepository;

        private readonly IDeletableEntityRepository<Adress> adressRepository;

        public CompanyService(
            IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<Adress> adressRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.companyRepository = companyRepository;
            this.adressRepository = adressRepository;
            this.userRepository = userRepository;
        }

        public async Task<string> CreateCompany(
            string companyName,
            string companyOwner,
            string identificationNumber,
            string bankAccount,
            string bankCode,
            string countryName,
            string cityName,
            string streetName,
            int streetNumber,
            string bankName)
        {
            var company = this.companyRepository
                .All()
                .FirstOrDefault(x => x.IdentificationNumber == identificationNumber);

            if (company == null)
            {
                company = new Company
                {
                    IdentificationNumber = identificationNumber,
                    BankName = bankName,
                    BankAccount = bankAccount,
                    BankCode = bankCode,
                    CompanyName = companyName,
                    CompanyOwner = companyOwner,
                    VatNumber = $"BG{identificationNumber}",
                    AdressId = await this.CreateAdress(countryName, cityName, streetName, streetNumber),
                };

                await this.companyRepository.AddAsync(company);
                await this.companyRepository.SaveChangesAsync();

                return company.Id;
            }

            return company.Id;
        }

        public async Task<string> CreateAdress(string countryName, string cityName, string streetName, int streetNumber)
        {
            var adress = new Adress
            {
                StreetName = streetName,
                StreetNumber = streetNumber,
                CityName = cityName,
                CountryName = countryName,
            };

            await this.adressRepository.AddAsync(adress);
            await this.adressRepository.SaveChangesAsync();

            return adress.Id;
        }

        public async Task<string> CreateCompany(AddBuyerViewModel model)
        {
            var company = this.companyRepository
                .All()
                .FirstOrDefault(x => x.IdentificationNumber == model.CompanyIdentificationNumber);

            if (company != null)
            {
                return company.Id;
            }

            company = new Company
            {
                IdentificationNumber = model.CompanyIdentificationNumber,
                BankName = model.BankName,
                BankAccount = model.BankAccount,
                BankCode = model.BankCode,
                CompanyName = model.CompanyName,
                CompanyOwner = model.CompanyOwner,
                VatNumber = $"BG{model.CompanyIdentificationNumber}",
                AdressId = await this.CreateAdress(
                    model.CountryName,
                    model.CountryName,
                    model.StreetName,
                    model.StreetNumber),
            };

            await this.companyRepository.AddAsync(company);
            await this.companyRepository.SaveChangesAsync();

            return company.Id;
        }
    }
}
