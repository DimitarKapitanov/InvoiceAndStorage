namespace InvoiceAndStorage.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Invoice;
    using InvoiceAndStorage.Web.ViewModels.SoldProducts;
    using Microsoft.EntityFrameworkCore;

    public class SoldProductService : ISoldProductService
    {
        private readonly IDeletableEntityRepository<SoldProduct> soldProductRepository;

        public SoldProductService(IDeletableEntityRepository<SoldProduct> soldProductRepository)
        {
            this.soldProductRepository = soldProductRepository;
        }

        public async Task<SoldProduct> CreateSoldProduct(Product invoiceProduct, int invoiceId, int quantity)
        {
            var soldProduct = new SoldProduct()
            {
                ProductName = invoiceProduct.Name,
                Qantity = quantity,
                SinglePrice = invoiceProduct.Price,
                TotalValue = quantity * invoiceProduct.Price,
                InvoiceId = invoiceId,
            };

            await this.soldProductRepository.AddAsync(soldProduct);
            await this.soldProductRepository.SaveChangesAsync();

            return soldProduct;
        }

        public async Task<InvoiceSoldProductsViewModel> GetAllSoldProducts(int id)
        {
            var invoiceDetails = await this.soldProductRepository
                .All()
                .Where(i => i.InvoiceId == id)
                .Include(i => i.Invoice)
                .ThenInclude(b => b.Buyer)
                .ThenInclude(c => c.Company)
                .Select(x => new SoldProductsVIewModel()
                {
                    ProductName = x.ProductName,
                    Quantity = x.Qantity,
                    TotalValue = x.TotalValue,
                    UnitPrice = x.SinglePrice,
                }).OrderBy(x => x.ProductName)
                .ToListAsync();

            var allSoldProducts = new InvoiceSoldProductsViewModel();

            foreach (var item in invoiceDetails)
            {
                allSoldProducts.SoldProducts.Add(item);
            }
            allSoldProducts.InvoiceNumber = id;
            allSoldProducts.TotalSum += allSoldProducts.SoldProducts.Sum(x => x.TotalValue);
            allSoldProducts.Vat = allSoldProducts.TotalSum * 0.2m;
            allSoldProducts.TotalValue = allSoldProducts.SoldProducts.Sum(x => x.TotalValue * 1.2m);

            return allSoldProducts;
        }
    }
}
