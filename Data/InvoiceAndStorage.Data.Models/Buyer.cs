namespace InvoiceAndStorage.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InvoiceAndStorage.Data.Common.Models;

    public class Buyer : BaseDeletableModel<string>
    {
        public Buyer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Product = new HashSet<Product>();
        }

        [Required]
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public string DatabaseОwnerId { get; set; }

        public virtual DatabaseОwner DatabaseОwner { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
