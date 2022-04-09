namespace InvoiceAndStorage.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Web.ViewModels.Product;

    public interface IProductService
    {
        public Task<bool> CreateProduct(AddProductViewModel addProductVewModel, string supplierId);

        public Task<bool> CreateProduct(AddProductWithoutVatNumberViewModel addProductVewModel, string supplierId);

        public Task<ICollection<ProductViewModel>> GetAllProducts(DatabaseОwner dbOwner);
    }
}
