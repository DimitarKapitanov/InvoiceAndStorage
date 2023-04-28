namespace InvoiceAndStorage.Data.Models
{
    using System;

    using InvoiceAndStorage.Data.Common.Models;

    public class DatabaseОwnersProducts : BaseDeletableModel<string>
    {
        public DatabaseОwnersProducts()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public string DatabaseОwnerId { get; set; }

        public DatabaseОwner DatabaseОwner { get; set; }
    }
}
