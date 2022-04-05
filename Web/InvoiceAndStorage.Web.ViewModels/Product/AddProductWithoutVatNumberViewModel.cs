namespace InvoiceAndStorage.Web.ViewModels.Product
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddProductWithoutVatNumberViewModel
    {
        [Required(ErrorMessage ="Полето е задължително")]
        [StringLength(30, ErrorMessage = "{0} на продукта трябва да е минимум {1} символ и {2} символа", MinimumLength = 2)]
        [Display(Name = "Име на продукта")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage ="Полето трябва да съдържа само полужителни числа")]
        [Display(Name = "Единична цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [Range(1, int.MaxValue, ErrorMessage = "Полето трябва да съдържа само полужителни числа по големи от 0")]
        [Display(Name = "Количество")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Полето ЕИК е задължително")]
        [Display(Name ="ЕИК на фирмата")]
        [RegularExpression("^([0-9]{9}|[0-9]{12})$", ErrorMessage = "{0} трябва да бъде между девет и дванадесет цифри")]
        public string CompanyIdentificationNumber { get; set; }
    }
}
