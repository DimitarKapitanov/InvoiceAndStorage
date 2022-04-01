namespace InvoiceAndStorage.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Web.ViewModels.Buyers;

    public interface ICompanyServise
    {
        Task<string> CreateCompany(
            string companyName,
            string companyOwner,
            string identificationNumber,
            string bankAccount,
            string bankCode,
            string countryName,
            string cityName,
            string streetName,
            int streetNumber,
            string bankName);

        Task<string> CreateAdress(string countryName, string cityName, string streetName, int streetNumber);

        Task<string> CreateCompany(AddBuyerViewModel model);

        Task<Company> GetCompany(string companyId);
    }
}
