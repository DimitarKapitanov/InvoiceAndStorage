namespace InvoiceAndStorage.Web.ViewModels.Product
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProductViewModel
    {
        [Display(Name = "Име на продукта")]
        public string Name { get; set; }

        [Display(Name = "Количесто на склад")]
        public int Amount { get; set; }

        [Display(Name = "Единична цена")]
        public decimal Price { get; set; }

        [Display(Name = "Име на доставчика")]
        public string SupplierName { get; set; }

        [Display(Name = "Дата на доставка")]
        public DateTime DeliveryDate { get; set; }
    }
}
