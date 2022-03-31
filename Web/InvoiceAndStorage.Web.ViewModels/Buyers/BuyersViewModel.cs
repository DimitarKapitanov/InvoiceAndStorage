namespace InvoiceAndStorage.Web.ViewModels.Buyers
{
    using System.ComponentModel.DataAnnotations;

    public class BuyersViewModel
    {
        [Display(Name = "ЕИК")]
        public string CompanyIdentificationNumber { get; set; }

        [Display(Name = "Име на фирмата")]
        public string CompanyName { get; set; }

        [Display(Name = "МОЛ")]
        public string CompanyOwner { get; set; }

        [Display(Name = "Държава по регистрация")]
        public string CountryName { get; set; }

        [Display(Name = "Град по регистрация")]
        public string CityName { get; set; }

        [Display(Name = "Име на улицата")]
        public string StreetName { get; set; }

        [Display(Name = "Номер на улицата")]
        public int StreetNumber { get; set; }
    }
}
