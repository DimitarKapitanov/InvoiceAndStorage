namespace InvoiceAndStorage.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Web.ViewModels.Invoice;
    using InvoiceAndStorage.Web.ViewModels.SoldProducts;

    public interface ISoldProductService
    {
        Task<SoldProduct> CreateSoldProduct(InvoiceProductViewModel invoiceProductViewModel, int id);

        Task<InvoiceSoldProductsViewModel> GetAllSoldProducts(int id);
    }
}
