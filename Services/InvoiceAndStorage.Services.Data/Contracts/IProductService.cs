namespace InvoiceAndStorage.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using InvoiceAndStorage.Web.ViewModels.Product;

    public interface IProductService
    {
        public Task CreateProduct(AddProductViewModel addProductVewModel);
    }
}
