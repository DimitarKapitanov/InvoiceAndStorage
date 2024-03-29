﻿namespace InvoiceAndStorage.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InvoiceAndStorage.Data.Common.Models;
    using InvoiceAndStorage.Data.Models.Enums;

    public class Invoice : BaseDeletableModel<int>
    {
        public Invoice()
        {
            this.SoldProducts = new List<SoldProduct>();
        }

        public decimal TotalInvoiceSum { get; set; }

        [Required]
        public string BuyerId { get; set; }

        public virtual Buyer Buyer { get; set; }

        [Required]
        public string DatabaseОwnerId { get; set; }

        public DatabaseОwner DatabaseОwner { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public InvoiceTipe InvoiceTipe { get; set; }

        public ICollection<SoldProduct> SoldProducts { get; set; }
    }
}
