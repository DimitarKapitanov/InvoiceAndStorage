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

        private readonly IDeletableEntityRepository<Country> countryRepository;

        private readonly IDeletableEntityRepository<City> cityRepository;

        private readonly IDeletableEntityRepository<Street> streetRepository;

        public CompanyService(
            IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<Adress> adressRepository,
            IDeletableEntityRepository<Country> countryRepository,
            IDeletableEntityRepository<City> cityRepository,
            IDeletableEntityRepository<Street> streetRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.companyRepository = companyRepository;
            this.adressRepository = adressRepository;
            this.countryRepository = countryRepository;
            this.cityRepository = cityRepository;
            this.streetRepository = streetRepository;
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
                StreetId = await this.CreateStreet(streetName, streetNumber),
                CityId = await this.CreateCity(cityName),
                CountryId = await this.CreateCountry(countryName),
            };

            await this.adressRepository.AddAsync(adress);
            await this.adressRepository.SaveChangesAsync();

            return adress.Id;
        }

        public async Task<string> CreateCountry(string countryName)
        {
            var county = this.countryRepository
                .All()
                .FirstOrDefault(x => x.CountryName == countryName);

            if (county != null)
            {
                return county.Id;
            }

            county = new Country()
            {
                CountryName = countryName,
            };

            await this.countryRepository.AddAsync(county);
            await this.countryRepository.AddAsync(county);

            return county.Id;
        }

        public async Task<string> CreateCity(string cityName)
        {
            var city = this.cityRepository
                .All()
                .FirstOrDefault(x => x.CityName == cityName);

            if (city != null)
            {
                return city.Id;
            }

            city = new City()
            {
                CityName = cityName,
            };

            await this.cityRepository.AddAsync(city);
            await this.cityRepository.SaveChangesAsync();

            return city.Id;
        }

        public async Task<string> CreateStreet(string streetName, int streetNumber)
        {
            var street = this.streetRepository
                .All()
                .FirstOrDefault(x => x.StreetName == streetName && x.StreetNumber == streetNumber);

            if (street != null)
            {
                return street.Id;
            }

            street = new Street()
            {
                StreetName = streetName,
                StreetNumber = streetNumber,
            };

            await this.streetRepository.AddAsync(street);
            await this.streetRepository.SaveChangesAsync();

            return street.Id;
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
