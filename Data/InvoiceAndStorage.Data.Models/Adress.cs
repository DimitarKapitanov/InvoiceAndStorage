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
        [MaxLength(100)]
        public string StreetName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int StreetNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string CityName { get; set; }

        [Required]
        [MaxLength(100)]
        public string CountryName { get; set; }
    }
}
