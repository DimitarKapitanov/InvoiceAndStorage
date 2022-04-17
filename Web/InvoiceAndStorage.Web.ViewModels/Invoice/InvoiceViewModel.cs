namespace InvoiceAndStorage.Web.ViewModels.Invoice
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class InvoiceViewModel
    {
        [Display(Name = "Дата на издаване")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Име на копувача")]
        public string BuyerName { get; set; }

        [Display(Name = "Номер на фактура")]
        public int InvoiceNumber { get; set; }

        [Display(Name = "Създадена от")]
        public string UserName { get; set; }

        [Display(Name = "Крайна цена")]
        public decimal TotalInvoiceSum { get; set; }
    }
}
