namespace InvoiceAndStorage.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class SoldProductController : Controller
    {
        private readonly ISoldProductService soldProductService;

        public SoldProductController(ISoldProductService soldProductService)
        {
            this.soldProductService = soldProductService;
        }

        public async Task<IActionResult> InvoiceSoldProducts()
        {
            var invoiceId = this.Request.RouteValues.Values.ToList();

            var allSoldProducts = await this.soldProductService.GetAllSoldProducts(int.Parse(invoiceId[2].ToString()));

            return this.View(allSoldProducts);
        }
    }
}
