namespace InvoiceAndStorage.Web.ViewModels.Buyers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddBuyerViewModel
    {
        [Required(ErrorMessage = "Полето {0} е задължително")]
        [Display(Name = "ЕИК на фирмата")]
        [RegularExpression("^([0-9]{9}|[0-9]{12})$", ErrorMessage = "{0} трябва да бъде между девет и дванадесет цифри")]
        public string CompanyIdentificationNumber { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително")]
        [Display(Name = "Име на фирмата")]
        [StringLength(50, ErrorMessage = "{0} трябва да е между {2} и {1} символа", MinimumLength = 2)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително")]
        [Display(Name = "Материално отговорно лице (Име и фамилия)")]
        [RegularExpression(@"^[А-Я]{1}[а-я]{2,20}\s[А-Я]{1}[а-я]{2,20}$", ErrorMessage = "Дължината на името или фамилията трябва да е между дава и двадесет символа")]
        public string CompanyOwner { get; set; }

        [Display(Name = "Име на банката")]
        [MaxLength(50)]
        public string BankName { get; set; }

        [Display(Name = "Банкова сметка")]
        [RegularExpression("^[BG]{2}[0-9]{2}[A-Z]{4}[0-9]{14}$", ErrorMessage = "Невалидна банкова сметка.")]
        public string BankAccount { get; set; }

        [Display(Name = "Банков код")]
        public string BankCode { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително")]
        [Display(Name = "Име на държават по регистраця на фирмата")]
        [StringLength(30, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа", MinimumLength = 3)]
        public string CountryName { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително")]
        [Display(Name = "Град по регистрация на фирмата")]
        [StringLength(30, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа", MinimumLength = 3)]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително")]
        [Display(Name = "Улица по регистрация на фирмата")]
        [StringLength(30, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа", MinimumLength = 3)]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително")]
        [Display(Name = "Номер на улицата по регистрация на фирмата")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} трябва да бъде между {2} и {1}")]
        public int StreetNumber { get; set; }
    }
}
