namespace InvoiceAndStorage.Web.ViewModels.Product
{
    using System;

    public class ProductViewModel
    {
        public string Name { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public string SupplierName { get; set; }

        public DateTime DeliveryDate { get; set; }
    }
}
