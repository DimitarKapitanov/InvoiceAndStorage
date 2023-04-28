namespace InvoiceAndStorage.Web.ViewModels.SoldProducts
{
    using System.ComponentModel.DataAnnotations;

    public class SoldProductsViewModel
    {
        [Display(Name = "Име на продукта")]
        public string ProductName { get; set; }

        [Display(Name = "Брой")]
        public int Quantity { get; set; }

        [Display(Name = "Единична цена")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Общо с ДДС")]
        public decimal TotalValue { get; set; }
    }
}
