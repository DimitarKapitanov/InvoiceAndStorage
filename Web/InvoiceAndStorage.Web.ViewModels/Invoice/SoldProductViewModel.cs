namespace InvoiceAndStorage.Web.ViewModels.Invoice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models.Enums;

    public class SoldProductViewModel
    {
        public int InvoiceNumber { get; set; }

        public string BuyerName { get; set; }

        public string SellerName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime InvoiceDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public InvoiceTipe InvoiceTipe { get; set; }

        // public ICollection<Product> Products { get; set; }
    }
}
