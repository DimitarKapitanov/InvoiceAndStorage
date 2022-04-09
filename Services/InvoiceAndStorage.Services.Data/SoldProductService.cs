namespace InvoiceAndStorage.Services.Data
{
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Invoice;

    public class SoldProductService : ISoldProductService
    {
        private readonly IDeletableEntityRepository<SoldProduct> soldProductRepository;

        public SoldProductService(IDeletableEntityRepository<SoldProduct> soldProductRepository)
        {
            this.soldProductRepository = soldProductRepository;
        }

        public async Task<SoldProduct> CreateSoldProduct(InvoiceProductViewModel invoiceProductViewModel, int id)
        {
            var soldProduct = new SoldProduct()
            {
                ProductName = invoiceProductViewModel.ProductName,
                Qantity = invoiceProductViewModel.Quantity,
                SinglePrice = invoiceProductViewModel.Price,
                InvoiceId = id,
                TotalValue = invoiceProductViewModel.Quantity * invoiceProductViewModel.Price,
            };

            await this.soldProductRepository.AddAsync(soldProduct);
            await this.soldProductRepository.SaveChangesAsync();

            return soldProduct;
        }
    }
}
