namespace InvoiceAndStorage.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using InvoiceAndStorage.Data.Common.Models;

    public class Company : BaseDeletableModel<string>
    {
        public Company()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyOwner { get; set; }

        public string VatNumber { get; set; }

        [Required]
        public string IdentificationNumber { get; set; }

        public string BankName { get; set; }

        public string BankAccount { get; set; }

        public string BankCode { get; set; }

        [Required]
        public string AdressId { get; set; }

        public Adress Adress { get; set; }

        public string BuyerId { get; set; }

        public Buyer Buyer { get; set; }

        public string DatabaseОwnerId { get; set; }

        public DatabaseОwner DatabaseОwner { get; set; }

        public string SupplierId { get; set; }

        public Supplier Supplier { get; set; }
    }
}
