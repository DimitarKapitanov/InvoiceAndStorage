namespace InvoiceAndStorage.Web.ViewModels.Supplier
{
    using System.Collections.Generic;

    public class AllSupplierViewModel
    {
        public ICollection<SuppliersViewModel> Suppliers { get; set; }
    }
}
