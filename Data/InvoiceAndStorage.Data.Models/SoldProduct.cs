namespace InvoiceAndStorage.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using InvoiceAndStorage.Data.Common.Models;

    public class SoldProduct : BaseDeletableModel<string>
    {
        public SoldProduct()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int Qantity { get; set; }

        [Required]
        public decimal SinglePrice { get; set; }

        [Required]
        public decimal TotalValue { get; set; }

        public int InvoiceId { get; set; }

        public Invoice Invoice { get; set; }
    }
}
