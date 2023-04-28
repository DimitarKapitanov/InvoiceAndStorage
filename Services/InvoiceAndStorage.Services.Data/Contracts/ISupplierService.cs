namespace InvoiceAndStorage.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Web.ViewModels.Supplier;

    public interface ISupplierService
    {
        Task<bool> CreateSupplire(AddSupplierViewModel addSupplierViewModel, string userId, string dataOwner);

        Task<ICollection<SuppliersViewModel>> AllSuppliers(string dbOwnerId);

        Task<Supplier> GetSupplierByIdentificationNumber(string identificationNumber, string ownerId);
    }
}
