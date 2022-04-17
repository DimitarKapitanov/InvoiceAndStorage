namespace InvoiceAndStorage.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Common;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Buyers;
    using InvoiceAndStorage.Web.ViewModels.Product;
    using InvoiceAndStorage.Web.ViewModels.Supplier;

    public class ValidViewModelsService : IValidViewModelsService
    {
        private readonly ISupplierSevice supplierSevice;

        public ValidViewModelsService(ISupplierSevice supplierSevice)
        {
            this.supplierSevice = supplierSevice;
        }

        public (bool IsValid, string Error) IsValidBuyerModel(AddBuyerViewModel model)
        {
            bool isValid = false;
            string error = string.Empty;

            if (!Regex.IsMatch(model.StreetName, GlobalConstants.ValiStreet))
            {
                error = $"Невалидна улица";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.CityName, GlobalConstants.ValiCity))
            {
                error = $"Невалиден град";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.CountryName, GlobalConstants.ValiCountyName))
            {
                error = $"Невалидна държава";
                return (isValid, error);
            }

            if (model.StreetNumber < 0 || model.StreetNumber > int.MaxValue)
            {
                error = $"Невалиден номер";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.CompanyName, GlobalConstants.ValidCompanyName))
            {
                error = $"Невалидна фирма";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.CompanyIdentificationNumber, GlobalConstants.ValidCompanyIdentificationNumber))
            {
                error = $"Невалиден ЕИК номер";
                return (isValid, error);
            }

            if (string.IsNullOrEmpty(model.CompanyOwner) || string.IsNullOrWhiteSpace(model.CompanyOwner) || !Regex.IsMatch(model.CompanyOwner, GlobalConstants.ValidCompanyOwner))
            {
                error = $"Невалиден МОЛ";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.BankAccount, GlobalConstants.ValidBankAccount))
            {
                error = $"Невалидна банкова сметка";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.BankCode, GlobalConstants.ValidBankCode))
            {
                error = $"Невалиден банков код";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.BankName, GlobalConstants.ValidBankName))
            {
                error = $"Невалидно име на банка";
                return (isValid, error);
            }

            isValid = true;

            return (isValid, error);
        }

        public (bool IsValid, string Error) IsValidProductModel(AddProductViewModel model)
        {
            bool isValid = false;
            string error = string.Empty;

            if (model == null)
            {
                error = "За да добавите продукт е нужно всички полета бъдат попълнени";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.Name, GlobalConstants.ValidProductName))
            {
                error = "Невалидно име на продукта!";
                return (isValid, error);
            }

            if (model.Amount < 0 || model.Amount > int.MaxValue)
            {
                error = "Количеството трябва да бъде само положително число!";
                return (isValid, error);
            }

            if (model.Price < 0 || model.Price > decimal.MaxValue)
            {
                error = "Цената трябва да бъде само положително число!";
                return (isValid, error);
            }

            isValid = true;

            return (isValid, error);
        }

        public (bool IsValid, string Error) IsValidProductWithoutVatNumberModel(AddProductWithoutVatNumberViewModel model)
        {
            bool isValid = false;
            string error = string.Empty;

            if (model == null)
            {
                error = "За да добавите продукт е нужно всички полета бъдат попълнени";
                return (isValid, error);
            }

            var supplier = this.supplierSevice.GetSupplierByIdentificationNumber(model.CompanyIdentificationNumber);

            if (supplier == null)
            {
                error = $"Несъществува доставчик с посоченото ЕИК";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.Name, GlobalConstants.ValidProductName))
            {
                error = "Невалидно име на продукта!";
                return (isValid, error);
            }

            if (model.Amount < 0 || model.Amount > int.MaxValue)
            {
                error = "Количеството трябва да бъде само положително число!";
                return (isValid, error);
            }

            if (model.Price < 0 || model.Price > decimal.MaxValue)
            {
                error = "Цената трябва да бъде само положително число!";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.CompanyIdentificationNumber, GlobalConstants.ValidCompanyIdentificationNumber))
            {
                error = "ЕИК на фирмата е грешен или не пълен";
                return (isValid, error);
            }

            isValid = true;

            return (isValid, error);
        }

        public (bool IsValid, string Error) IsValidSupplierModel(AddSupplierViewModel model)
        {
            bool isValid = false;
            string error = string.Empty;

            if (!Regex.IsMatch(model.StreetName, GlobalConstants.ValiStreet))
            {
                error = $"Невалидна улица";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.CityName, GlobalConstants.ValiCity))
            {
                error = $"Невалиден град";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.CountryName, GlobalConstants.ValiCountyName))
            {
                error = $"Невалидна държава";
                return (isValid, error);
            }

            if (model.StreetNumber < 0 || model.StreetNumber > int.MaxValue)
            {
                error = $"Невалиден номер";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.CompanyName, GlobalConstants.ValidCompanyName))
            {
                error = $"Невалидна фирма";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.CompanyIdentificationNumber, GlobalConstants.ValidCompanyIdentificationNumber))
            {
                error = $"Невалиден ЕИК номер";
                return (isValid, error);
            }

            if (string.IsNullOrEmpty(model.CompanyOwner) || string.IsNullOrWhiteSpace(model.CompanyOwner) || !Regex.IsMatch(model.CompanyOwner, GlobalConstants.ValidCompanyOwner))
            {
                error = $"Невалиден МОЛ";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.BankAccount, GlobalConstants.ValidBankAccount))
            {
                error = $"Невалидна банкова сметка";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.BankCode, GlobalConstants.ValidBankCode))
            {
                error = $"Невалиден банков код";
                return (isValid, error);
            }

            if (!Regex.IsMatch(model.BankName, GlobalConstants.ValidBankName))
            {
                error = $"Невалидно име на банка";
                return (isValid, error);
            }

            isValid = true;

            return (isValid, error);
        }
    }
}
