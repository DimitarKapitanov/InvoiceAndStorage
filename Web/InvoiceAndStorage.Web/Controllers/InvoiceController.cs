namespace InvoiceAndStorage.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Invoice;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IInvoiceService invoiceService;
        private readonly IDeletableEntityRepository<DatabaseОwner> dataBaseOwnerRepository;
        private readonly IProductService productService;

        public InvoiceController(
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<DatabaseОwner> dataBaseOwnerRepository,
            IProductService productService,
            IInvoiceService invoiceService)
        {
            this.userManager = userManager;
            this.dataBaseOwnerRepository = dataBaseOwnerRepository;
            this.productService = productService;
            this.invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateInvoice()
        {
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
            if (!this.ModelState.IsValid)
            {
                return this.View(createInvoiceModel);
            }

            var userId = this.userManager.GetUserId(this.User);

            var user = await this.dataBaseOwnerRepository.All().Select(x => x.ApplicationUsers.FirstOrDefault(u => u.Id == userId)).FirstOrDefaultAsync();

            var dbOwner = await this.dataBaseOwnerRepository.All().FirstOrDefaultAsync(d => d.Id == user.DatabaseОwnerId);

            var products = await this.invoiceService.GetAllInvoiceProducts(dbOwner);

            var (isValid, error) = await this.invoiceService.AddInvoice(createInvoiceModel, userId);

            if (!isValid)
            {
                this.TempData["CreateInvoice"] = error;
                return this.View("CreateInvoice", products);
            }

            return this.Redirect("/Invoice/AllInvoice");
        }

        public async Task<IActionResult> AllInvoice()
        {
            var userId = this.userManager.GetUserId(this.User);

            var invoice = await this.invoiceService.GetAllInvoice(userId);

            return this.View(invoice);
        }
    }
}
