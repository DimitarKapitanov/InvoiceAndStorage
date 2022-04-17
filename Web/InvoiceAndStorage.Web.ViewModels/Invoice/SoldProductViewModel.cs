namespace InvoiceAndStorage.Web.ViewModels.Invoice
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using InvoiceAndStorage.Data.Models.Enums;

    public class SoldProductViewModel
    {
        [Display(Name = "Номер на фактура")]
        public int InvoiceNumber { get; set; }

        [Display(Name = "Име на копувача")]
        public string BuyerName { get; set; }

        [Display(Name = "Име на продавача")]
        public string SellerName { get; set; }

        [Display(Name = "Създадена от")]
        public string CreatedBy { get; set; }

        [Display(Name = "Дата на въчване")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Дата на издаване")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Нчин на плащане")]
        public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "Вид на документа")]
        public InvoiceTipe InvoiceTipe { get; set; }

        // public ICollection<Product> Products { get; set; }
    }
}
