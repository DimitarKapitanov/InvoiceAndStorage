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
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }

        public DateTime DeliveryDate { get; set; }

        [Required]
        public string SupplierId { get; set; }

        public Supplier Supplier { get; set; }
    }
}
