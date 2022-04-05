namespace InvoiceAndStorage.Web.ViewModels.Product
{
    using System.Collections.Generic;

    public class AllProductsViewModel
    {
        public ICollection<ProductViewModel> Products { get; set; }
    }
}
