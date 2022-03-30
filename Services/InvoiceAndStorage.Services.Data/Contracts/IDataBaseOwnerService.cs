namespace InvoiceAndStorage.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IDataBaseOwnerService
    {
        Task<string> CreateDataBaseOwner(string companyId);

        Task AddUser(string id, string databaseOwnerId);

        Task<string> AddBuyer(string buyerId, string databaseOwnerId);
    }
}
