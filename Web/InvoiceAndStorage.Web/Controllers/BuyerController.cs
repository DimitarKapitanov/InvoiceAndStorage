namespace InvoiceAndStorage.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Buyers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BuyerController : BaseController
    {
        private readonly IBuyerService buyerService;
        private readonly IDataBaseOwnerService databaseOwner;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<DatabaseОwner> dataBaseOwnerRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public BuyerController(
            IBuyerService buyerService,
            IDataBaseOwnerService databaseOwner,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<DatabaseОwner> dataBaseOwnerRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.buyerService = buyerService;
            this.databaseOwner = databaseOwner;
            this.userManager = userManager;
            this.dataBaseOwnerRepository = dataBaseOwnerRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult AddBuyers() => this.View();

        [HttpPost]
        public async Task<IActionResult> AddBuyers(AddBuyerViewModel addBuyerViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(addBuyerViewModel);
            }

            var user = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var isCreate = await this.buyerService.CreateBuyer(addBuyerViewModel, user);

            if (!isCreate)
            {
                this.TempData["AddBuyers"] = $"Фирмата вече съществува";
                return this.View(addBuyerViewModel);
            }

            return this.Redirect("/Buyer/All");
        }

        public async Task<IActionResult> All()
        {
            var userId = this.userManager.GetUserId(this.User);

            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);

            var dbOwner = this.dataBaseOwnerRepository.All().FirstOrDefault();

            var buyers = await this.buyerService.All(dbOwner.Id);

            return this.View("All", buyers);
        }
    }
}
