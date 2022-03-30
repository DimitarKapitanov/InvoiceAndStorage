namespace InvoiceAndStorage.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InvoiceAndStorage.Data.Common.Models;

    public class Adress : BaseDeletableModel<string>
    {
        public Adress()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [ForeignKey(nameof(Id))]
        public string CountryId { get; set; }

        public virtual Country Country { get; set; }

        [Required]
        [ForeignKey(nameof(Id))]
        public string CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        [ForeignKey(nameof(Id))]
        public string StreetId { get; set; }

        public virtual Street Street { get; set; }
    }
}
