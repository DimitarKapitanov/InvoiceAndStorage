namespace InvoiceAndStorage.Services.Data.Contracts
{
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Web.ViewModels.Invoice;
    using System.Threading.Tasks;

    public interface IInvoiceService
    {
        InvoiceViewModel GetAllInvoiceProducts(DatabaseОwner dbOwner);

        Task<bool> AddInvoice(InvoiceViewModel invoiceViewModel, DatabaseОwner dbOwner, ApplicationUser user);

    }
}
