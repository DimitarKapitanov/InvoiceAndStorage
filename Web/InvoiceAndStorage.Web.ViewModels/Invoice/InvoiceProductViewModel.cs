namespace InvoiceAndStorage.Web.ViewModels.Invoice
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class InvoiceProductViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Име на продукта")]
        public string ProductName { get; set; }

        [Display(Name = "Количество на склад")]
        public string Amount { get; set; }

        [Display(Name = "Единична цена")]
        public string Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage ="Количеството тярбва да е полужително число")]
        [Display(Name ="Количество")]
        public int Quantity { get; set; }
    }
}
