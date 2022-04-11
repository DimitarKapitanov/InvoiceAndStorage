namespace InvoiceAndStorage.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Invoice;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class InvoiceController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IInvoiceService invoiceService;
        private readonly IDeletableEntityRepository<DatabaseОwner> dataBaseOwnerRepository;

        public InvoiceController(
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<DatabaseОwner> dataBaseOwnerRepository,
            IInvoiceService invoiceService)
        {
            this.userManager = userManager;
            this.dataBaseOwnerRepository = dataBaseOwnerRepository;
            this.invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateInvoice()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            var userId = this.userManager.GetUserId(this.User);

            var user = await this.dataBaseOwnerRepository.All().Select(x => x.ApplicationUsers.FirstOrDefault(u => u.Id == userId)).FirstOrDefaultAsync();

            var dbOwner = await this.dataBaseOwnerRepository.All().FirstOrDefaultAsync(d => d.Id == user.DatabaseОwnerId);

            var products = await this.invoiceService.GetAllInvoiceProducts(dbOwner);

            var invoice = new CreateInvoiceViewModel
            {
                InvoiceProductViewModels = new List<InvoiceProductViewModel>(),
            };

            invoice = products;

            return this.View(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice(CreateInvoiceViewModel createInvoiceModel)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            var userId = this.userManager.GetUserId(this.User);

            var (isValid, error) = await this.invoiceService.AddInvoice(createInvoiceModel, userId);

            if (!isValid)
            {
                this.TempData["CreateInvoice"] = error;
                return this.View("CreateInvoice", createInvoiceModel);
            }

            return this.Redirect("/");
        }

        public async Task<IActionResult> AllInvoice()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            var userId = this.userManager.GetUserId(this.User);

            var invoice = await this.invoiceService.GetAllInvoice(userId);

            return this.View(invoice);
        }
    }
}
