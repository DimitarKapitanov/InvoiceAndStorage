namespace InvoiceAndStorage.Web.ViewModels.Invoice
{
    public class InvoiceProductViewModel
    {
        public string ProductName { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
