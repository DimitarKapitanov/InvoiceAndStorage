namespace InvoiceAndStorage.Web.ViewModels.Invoice
{
    using System;

    public class InvoiceViewModel
    {
        public DateTime InvoiceDate { get; set; }

        public string BuyerName { get; set; }

        public int InvoiceNumber { get; set; }

        public string UserName { get; set; }

        public decimal TotalInvoiceSum { get; set; }
    }
}
