using System.ComponentModel.DataAnnotations;

namespace InvoiceAndStorage.Web.ViewModels.SoldProducts
{
    public class SoldProductsVIewModel
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
