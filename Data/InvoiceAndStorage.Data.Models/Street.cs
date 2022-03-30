namespace InvoiceAndStorage.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InvoiceAndStorage.Data.Common.Models;

    public class Street : BaseDeletableModel<string>
    {
        public Street()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string StreetName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int StreetNumber { get; set; }
    }
}
