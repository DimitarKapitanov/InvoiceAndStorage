namespace InvoiceAndStorage.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Web.ViewModels.Invoice;

    public interface IInvoiceService
    {
        Task<CreateInvoiceViewModel> GetAllInvoiceProducts(DatabaseОwner dbOwner);

        Task<(bool IsValid, string Error)> AddInvoice(CreateInvoiceViewModel product, string userId);

        Task<ICollection<Product>> GetProducts(string dataOwnerId);

        Task<AllInvoice> GetAllInvoice(string userId);
    }
}
