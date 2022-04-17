namespace InvoiceAndStorage.Web.ViewModels.Invoice
{
    using System.Collections.Generic;

    public class AllInvoiceViewModel
    {
        public AllInvoiceViewModel()
        {
            this.InvoiceViewModels = new List<InvoiceViewModel>();
        }

        public IList<InvoiceViewModel> InvoiceViewModels { get; set; }
    }
}
