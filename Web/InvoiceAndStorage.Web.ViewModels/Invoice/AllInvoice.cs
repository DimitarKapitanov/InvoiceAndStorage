namespace InvoiceAndStorage.Web.ViewModels.Invoice
{
    using System.Collections.Generic;

    public class AllInvoice
    {
        public AllInvoice()
        {
            this.InvoiceViewModels = new List<InvoiceViewModel>();
        }

        public IList<InvoiceViewModel> InvoiceViewModels { get; set; }

    }
}
