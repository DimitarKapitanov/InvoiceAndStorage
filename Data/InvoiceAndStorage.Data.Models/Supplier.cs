namespace InvoiceAndStorage.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InvoiceAndStorage.Data.Common.Models;

    public class Supplier : BaseDeletableModel<string>
    {
        public Supplier()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new HashSet<Product>();
        }

        [Required]
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }

        [Required]
        public string DatabaseОwnerId { get; set; }

        public DatabaseОwner DatabaseОwner { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
