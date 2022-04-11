namespace InvoiceAndStorage.Web.ViewModels.SoldProducts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InvoiceSoldProductsViewModel
    {
        public InvoiceSoldProductsViewModel()
        {
            this.SoldProducts = new List<SoldProductsVIewModel>();
        }

        [Display(Name ="ДДС")]
        public int Vat { get; set; }

        [Display(Name = "Сума с ДДС")]
        public decimal TotalValue { get; set; }

        public IList<SoldProductsVIewModel> SoldProducts { get; set; }
    }
}
