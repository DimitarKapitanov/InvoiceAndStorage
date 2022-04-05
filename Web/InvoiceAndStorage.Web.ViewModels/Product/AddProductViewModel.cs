namespace InvoiceAndStorage.Web.ViewModels.Product
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddProductViewModel
    {
        [Required(ErrorMessage = "Полето е задължително")]
        [StringLength(30, ErrorMessage = "{0} на продукта трябва да е минимум {1} символ и {2} символа", MinimumLength = 2)]
        [Display(Name = "Име на продукта")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Полето трябва да съдържа само полужителни числа")]
        [Display(Name = "Единична цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [Range(0, int.MaxValue, ErrorMessage = "Полето трябва да съдържа само полужителни числа")]
        [Display(Name = "Количество")]
        public int Amount { get; set; }
    }
}
