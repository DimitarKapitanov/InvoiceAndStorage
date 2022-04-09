namespace InvoiceAndStorage.Web.ViewModels.Invoice
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InvoiceAndStorage.Data.Models.Enums;

    public class CreateInvoiceViewModel
    {
        [Required]
        [Display(Name ="ЕИК на клиента")]
        public string BuyerIdentificationNumber { get; set; }

        [Required]
        [Display(Name ="Начин на плащане")]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        [Display(Name ="Вид на фактурата")]
        public InvoiceTipe InvoiceTipe { get; set; }

        public IList<InvoiceProductViewModel> InvoiceProductViewModels { get; set; }
    }
}
