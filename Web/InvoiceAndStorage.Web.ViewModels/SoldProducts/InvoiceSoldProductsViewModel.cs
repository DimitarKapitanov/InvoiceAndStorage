namespace InvoiceAndStorage.Web.ViewModels.SoldProducts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InvoiceSoldProductsViewModel
    {
        public InvoiceSoldProductsViewModel()
        {
            this.SoldProducts = new List<SoldProductsViewModel>();
        }

        public int InvoiceNumber { get; set; }

        [Display(Name = "Сума без ДДС")]
        public decimal TotalSum { get; set; }

        [Display(Name ="ДДС")]
        public decimal Vat { get; set; }

        [Display(Name = "Сума с ДДС")]
        public decimal TotalValue { get; set; }

        public IList<SoldProductsViewModel> SoldProducts { get; set; }
    }
}
