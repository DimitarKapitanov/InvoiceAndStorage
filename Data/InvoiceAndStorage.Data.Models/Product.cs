namespace InvoiceAndStorage.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InvoiceAndStorage.Data.Common.Models;

    public class Product : BaseDeletableModel<string>
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), "0m", "79228162514264337593543950335m")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 2147483647)]
        public int Amount { get; set; }

        public DateTime DeliveryDate { get; set; }

        [Required]
        public string SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public string BuyerId { get; set; }

        public Buyer Buyer { get; set; }
    }
}
