namespace InvoiceAndStorage.Web.Controllers
{
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

        public async Task<IActionResult> CreateInvoice()
        {
            var userId = this.userManager.GetUserId(this.User);

            var user = await this.dataBaseOwnerRepository.All().Select(x => x.ApplicationUsers.FirstOrDefault(u => u.Id == userId)).FirstOrDefaultAsync();

            var dbOwner = await this.dataBaseOwnerRepository.All().FirstOrDefaultAsync(d => d.Id == user.DatabaseОwnerId);

            var products = this.invoiceService.GetAllInvoiceProducts(dbOwner);

            return this.View(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice(InvoiceViewModel createInvoiceModel)
        {
            var userId = this.userManager.GetUserId(this.User);

            var user = await this.dataBaseOwnerRepository.All().Select(x => x.ApplicationUsers.FirstOrDefault(u => u.Id == userId)).FirstOrDefaultAsync();

            var dbOwner = await this.dataBaseOwnerRepository.All().FirstOrDefaultAsync(d => d.Id == user.DatabaseОwnerId);

            var isCreated = this.invoiceService.AddInvoice(createInvoiceModel, dbOwner, user);



            return this.View();
        }
    }
}
