namespace InvoiceAndStorage.Web.ViewModels.Invoice
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InvoiceViewModel
    {
        public InvoiceViewModel()
        {
            this.InvoiceProductViewModels = new HashSet<InvoiceProductViewModel>();
        }

        [Required]
        public string BuyerIdentificationNumber { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public string InvoiceTipe { get; set; }

        public ICollection<InvoiceProductViewModel> InvoiceProductViewModels { get; set; }
    }
}
