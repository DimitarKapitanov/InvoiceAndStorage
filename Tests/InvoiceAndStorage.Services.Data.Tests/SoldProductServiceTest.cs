namespace InvoiceAndStorage.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Data.Repositories;
    using InvoiceAndStorage.Services.Data.Common;
    using Xunit;

    public class SoldProductServiceTest
    {
        [Fact]
        public async Task GetAllSoldProducts_ByInvoiceId_ShouldReturnAllInvocieSoldProduct()
        {
            MapperInitializer.InitializeMapper();
            var context = InitializeContext.CreateContextForInMemory();
            var soldProductRepository = new EfDeletableEntityRepository<SoldProduct>(context);

            var soldProductService = new SoldProductService(soldProductRepository);

            var company = new Company
            {
                Id = "2",
                IdentificationNumber = "111111112",
                VatNumber = "BG111111111",
                BankAccount = "BG11AAAA12345678912345",
                CompanyOwner = "Ганчо Ганев",
                CompanyName = "Пешо ООД",
                Adress = new Adress()
                {
                    CityName = "Казанлък",
                    CountryName = "България",
                    Id = "1",
                    StreetName = "Раковски",
                    StreetNumber = 5,
                },
            };

            await context.AddAsync(company);
            await context.SaveChangesAsync();

            var buyer = new Buyer
            {
               CompanyId = "2",
               Invoices = new List<Invoice>(),
            };

            await context.AddAsync(buyer);
            await context.SaveChangesAsync();

            company.BuyerId = buyer.Id;

            var invoice = new Invoice()
            {
                Id = 2,
                ApplicationUserId = "2",
                BuyerId = buyer.Id,
                DatabaseОwnerId = "A1",
                SoldProducts = new List<SoldProduct>(),
            };

            var soldproduct = new SoldProduct()
            {
                InvoiceId = invoice.Id,
                ProductName = "Banan",
                Qantity = 5,
                SinglePrice = 2.23M,
                TotalValue = default,
            };

            await context.AddAsync(soldproduct);
            await context.SaveChangesAsync();

            invoice.SoldProducts.Add(soldproduct);
            invoice.SoldProducts.Add(soldproduct);
            invoice.SoldProducts.Add(soldproduct);

            buyer.Invoices.Add(invoice);

            await context.AddAsync(invoice);
            await context.SaveChangesAsync();

            var soldProducts = await soldProductService.GetAllSoldProducts(invoice.Id);

            Assert.NotNull(soldProducts);
        }
    }
}
