namespace InvoiceAndStorage.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Web.ViewModels.Invoice;
    using InvoiceAndStorage.Web.ViewModels.SoldProducts;

    public interface ISoldProductService
    {
        Task<SoldProduct> CreateSoldProduct(Product invoiceProduct, int invoiceId, int quantity);

        Task<InvoiceSoldProductsViewModel> GetAllSoldProducts(int id);
    }
}
