namespace InvoiceAndStorage.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SoldProductController : Controller
    {
        private readonly ISoldProductService soldProductService;
        private readonly UserManager<ApplicationUser> userManager;

        public SoldProductController(ISoldProductService soldProductService, UserManager<ApplicationUser> userManager)
        {
            this.soldProductService = soldProductService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> InvoiceSoldProducts()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            var invoiceId = this.Request.RouteValues.Values.ToList();

            var allSoldProducts = await this.soldProductService.GetAllSoldProducts(int.Parse(invoiceId[2].ToString()));

            return this.View(allSoldProducts);
        }
    }
}
