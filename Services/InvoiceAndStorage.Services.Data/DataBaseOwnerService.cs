﻿namespace InvoiceAndStorage.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class DataBaseOwnerService : IDataBaseOwnerService
    {
        private readonly IDeletableEntityRepository<DatabaseОwner> dbRepository;
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<Buyer> buyerRepository;

        public DataBaseOwnerService(
            IDeletableEntityRepository<DatabaseОwner> repository,
            IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<Buyer> buyerRepository)
        {
            this.dbRepository = repository;
            this.companyRepository = companyRepository;
            this.buyerRepository = buyerRepository;
        }

        public async Task<string> AddBuyer(string buyerId, string databaseOwnerId)
        {
            var databaseOwner = this.dbRepository.All().FirstOrDefault(d => d.Id == databaseOwnerId);

            var buyer = this.buyerRepository.All().FirstOrDefault(b => b.Id == buyerId);

            databaseOwner.Buyers.Add(buyer);

            this.dbRepository.Update(databaseOwner);
            await this.dbRepository.SaveChangesAsync();
            return databaseOwner.Id;
        }

        public async Task AddUser(ApplicationUser user, string databaseOwnerId)
        {
            var databaseOwner = this.dbRepository
                .All()
                .FirstOrDefault(d => d.ApplicationUsers
                                    .FirstOrDefault(x => x.Id == user.Id)
                                    .DatabaseОwnerId == databaseOwnerId);

            databaseOwner.ApplicationUsers.Add(user);

            await this.dbRepository.SaveChangesAsync();
        }

        public async Task<string> CreateDataBaseOwner(string companyId)
        {
            var companyRepo = this.companyRepository
                .All()
                .FirstOrDefault(x => x.Id == companyId);

            var databaseOwner = new DatabaseОwner()
            {
                CompanyId = companyRepo.Id,
            };

            companyRepo.DatabaseОwnerId = databaseOwner.Id;

            await this.dbRepository.AddAsync(databaseOwner);
            this.companyRepository.Update(companyRepo);
            await this.dbRepository.SaveChangesAsync();

            return databaseOwner.Id;
        }

        public async Task<string> GetDatabaseОwner(string userId)
        {
            var owners = await this.dbRepository
                .All()
                .Select(x => x.ApplicationUsers.FirstOrDefault(i => i.Id == userId)).FirstOrDefaultAsync();

            var dbOwner = this.dbRepository.All().FirstOrDefault(x => x.ApplicationUsers.FirstOrDefault(u => u.Id == userId).DatabaseОwnerId == owners.DatabaseОwnerId);

            return dbOwner.Id;
        }
    }
}
