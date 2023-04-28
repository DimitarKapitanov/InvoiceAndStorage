namespace InvoiceAndStorage.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InvoiceAndStorage.Data.Common.Models;

    public class DatabaseОwner : BaseDeletableModel<string>
    {
        public DatabaseОwner()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ApplicationUsers = new HashSet<ApplicationUser>();
            this.Invoices = new HashSet<Invoice>();
            this.Suppliers = new HashSet<Supplier>();
            this.Buyers = new HashSet<Buyer>();
        }

        [Required]
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public virtual ICollection<Buyer> Buyers { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<DatabaseОwnersProducts> DatabaseОwnersProducts { get; set; }
    }
}
