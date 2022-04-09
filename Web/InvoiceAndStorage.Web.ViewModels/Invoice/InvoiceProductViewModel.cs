namespace InvoiceAndStorage.Web.ViewModels.Invoice
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class InvoiceProductViewModel
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
