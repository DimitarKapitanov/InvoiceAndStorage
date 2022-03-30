namespace InvoiceAndStorage.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InvoiceAndStorage.Data.Common.Models;

    public class City : BaseDeletableModel<string>
    {
        public City()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Streets = new HashSet<Street>();
        }

        [Required]
        public string CityName { get; set; }

        public virtual ICollection<Street> Streets { get; set; }
    }
}
