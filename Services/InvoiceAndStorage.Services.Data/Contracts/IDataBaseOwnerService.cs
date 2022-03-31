namespace InvoiceAndStorage.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;

    public interface IDataBaseOwnerService
    {
        Task<string> CreateDataBaseOwner(string companyId);

        Task AddUser(ApplicationUser user, string databaseOwnerId);

        Task<string> AddBuyer(string buyerId, string databaseOwnerId);
    }
}
