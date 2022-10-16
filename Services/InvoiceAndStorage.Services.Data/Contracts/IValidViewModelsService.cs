namespace InvoiceAndStorage.Services.Data.Contracts
{
    using InvoiceAndStorage.Web.ViewModels.Buyers;
    using InvoiceAndStorage.Web.ViewModels.Product;
    using InvoiceAndStorage.Web.ViewModels.Supplier;

    public interface IValidViewModelsService
    {
        public (bool IsValid, string Error) IsValidBuyerModel(AddBuyerViewModel model);

        public (bool IsValid, string Error) IsValidSupplierModel(AddSupplierViewModel model, string ownerId);

        public (bool IsValid, string Error) IsValidProductModel(AddProductViewModel model, string ownerId);

        public (bool IsValid, string Error) IsValidProductWithoutVatNumberModel(AddProductWithoutVatNumberViewModel model, string ownerId);
    }
}
