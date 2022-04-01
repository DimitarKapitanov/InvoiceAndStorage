namespace InvoiceAndStorage.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Web.ViewModels.Buyers;

    public interface IBuyerService
    {
        Task<ICollection<BuyersViewModel>> All(string dbOwnerId);

        Task<bool> CreateBuyer(AddBuyerViewModel model, string userId, string dataOwner);
    }
}
