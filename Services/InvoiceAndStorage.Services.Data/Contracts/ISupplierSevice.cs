namespace InvoiceAndStorage.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Web.ViewModels.Supplier;

    public interface ISupplierSevice
    {
        Task<bool> CreateSupplire(AddSupplierViewModel addSupplierViewModel, string userId, string dataOwner);

        Task<ICollection<SuppliersViewModel>> AllSuppliers(string dbOwnerId);

        Task<Supplier> GetSupplierByIdentificationNumber(string identificationNumber);
    }
}
