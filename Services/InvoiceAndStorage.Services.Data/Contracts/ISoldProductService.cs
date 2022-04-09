namespace InvoiceAndStorage.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Web.ViewModels.Invoice;

    public interface ISoldProductService
    {
        Task<SoldProduct> CreateSoldProduct(InvoiceProductViewModel invoiceProductViewModel, int id);
    }
}
