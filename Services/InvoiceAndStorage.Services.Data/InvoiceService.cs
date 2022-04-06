namespace InvoiceAndStorage.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Invoice;

    public class InvoiceService : IInvoiceService
    {
        private readonly IDeletableEntityRepository<Supplier> supplierRepository;

        public InvoiceService(IDeletableEntityRepository<Supplier> supplierRepository)
        {
            this.supplierRepository = supplierRepository;
        }

        public Task<bool> AddInvoice(InvoiceViewModel invoiceViewModel, DatabaseОwner dbOwner, ApplicationUser user)
        {
            var model = invoiceViewModel;

            return default;
        }

        public InvoiceViewModel GetAllInvoiceProducts(DatabaseОwner dbOwner)
        {
            var productsBySupplier = this.supplierRepository.All()
                .Where(x => x.DatabaseОwnerId == dbOwner.Id)
                .Select(p => p.Products
                    .Select(a => new
                    {
                        a.Name,
                        a.Amount,
                        a.Price,
                        SupplierName = p.Company.CompanyName,
                        DeliveryDate = DateTime.UtcNow.Date,
                    }).ToList()).ToList();

            var products = new InvoiceViewModel();

            foreach (var currProducts in productsBySupplier)
            {
                foreach (var item in currProducts)
                {
                    var productViewModel = new InvoiceProductViewModel
                    {
                        ProductName = item.Name,
                        Amount = item.Amount,
                        Price = item.Price,
                    };

                    products.InvoiceProductViewModels.Add(productViewModel);
                }
            }

            return products;
        }
    }
}
