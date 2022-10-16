namespace InvoiceAndStorage.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Supplier;
    using Microsoft.EntityFrameworkCore;

    public class SupplireService : ISupplierSevice
    {
        private readonly ICompanyServise companyServise;
        private readonly IDeletableEntityRepository<DatabaseОwner> dataBaseOwner;
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<Supplier> supplierRepository;

        public SupplireService(
            IDeletableEntityRepository<DatabaseОwner> dataBaseOwner,
            ICompanyServise companyServise,
            IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<Supplier> supplierRepository)
        {
            this.dataBaseOwner = dataBaseOwner;
            this.companyServise = companyServise;
            this.companyRepository = companyRepository;
            this.supplierRepository = supplierRepository;
        }

        public async Task<bool> CreateSupplire(AddSupplierViewModel addSupplierViewModel, string userId, string dataOwner)
        {
            var dbOwner = this.dataBaseOwner
                .All()
                .FirstOrDefault(x => x.ApplicationUsers.FirstOrDefault(i => i.Id == userId).DatabaseОwnerId == dataOwner);

            var supplier = dbOwner.Suppliers.FirstOrDefault(x => x.DatabaseОwnerId == dataOwner);

            var companyId = await this.companyServise.CreateCompany(addSupplierViewModel);

            var company = await this.companyServise.GetCompany(companyId);

            var isCreate = false;

            if (supplier == null)
            {
                supplier = new Supplier
                {
                    CompanyId = company.Id,
                    DatabaseОwnerId = dbOwner.Id,
                };

                dbOwner.Suppliers.Add(supplier);

                company.DatabaseОwnerId = dbOwner.Id;
                company.SupplierId = supplier.Id;

                this.dataBaseOwner.Update(dbOwner);
                this.companyRepository.Update(company);

                await this.dataBaseOwner.SaveChangesAsync();

                isCreate = true;

                return isCreate;
            }

            return isCreate;
        }

        public async Task<ICollection<SuppliersViewModel>> AllSuppliers(string dbOwnerId)
        {
            var allSuppliers = await this.supplierRepository.All()
                .Where(x => x.DatabaseОwnerId == dbOwnerId)
                .Select(x => new SuppliersViewModel()
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

            return allSuppliers;
        }

        public async Task<Supplier> GetSupplierByIdentificationNumber(string identificationNumber, string ownerId)
        {
            var supplier = await this.supplierRepository
                .All()
                .Where(x => x.DatabaseОwnerId == ownerId)
                .FirstOrDefaultAsync(x => x.Company.IdentificationNumber == identificationNumber);

            return supplier;
        }

        public object GetSupplierByIdentificationNumber(Func<object, bool> p)
        {
            throw new System.NotImplementedException();
        }
    }
}
