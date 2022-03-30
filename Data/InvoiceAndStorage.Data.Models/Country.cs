namespace InvoiceAndStorage.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InvoiceAndStorage.Data.Common.Models;

    public class Country : BaseDeletableModel<string>
    {
        public Country()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cities = new HashSet<City>();
        }

        [Required]
        public string CountryName { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
