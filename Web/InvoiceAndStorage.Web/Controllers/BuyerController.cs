namespace InvoiceAndStorage.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Buyers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class BuyerController : BaseController
    {
        private readonly IBuyerService buyerService;
        private readonly IValidViewModelsService validViewModelsService;
        private readonly IDataBaseOwnerService databaseOwner;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<DatabaseОwner> dataBaseOwnerRepository;

        public BuyerController(
            IBuyerService buyerService,
            IDataBaseOwnerService databaseOwner,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<DatabaseОwner> dataBaseOwnerRepository,
            IValidViewModelsService validViewModelsService)
        {
            this.buyerService = buyerService;
            this.databaseOwner = databaseOwner;
            this.userManager = userManager;
            this.dataBaseOwnerRepository = dataBaseOwnerRepository;
            this.validViewModelsService = validViewModelsService;
        }

        [HttpGet]
        public IActionResult AddBuyers()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBuyers(AddBuyerViewModel addBuyerViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(addBuyerViewModel);
            }

            var isValid = this.validViewModelsService.IsValidBuyerModel(addBuyerViewModel);

            if (!isValid.IsValid)
            {
                this.TempData["AddBuyers"] = isValid.Error;

                return this.View(addBuyerViewModel);
            }

            var user = this.userManager.GetUserId(this.User);

            var owner = await this.databaseOwner.GetDatabaseОwner(user);

            var isCreate = await this.buyerService.CreateBuyer(addBuyerViewModel, user, owner);

            if (!isCreate)
            {
                this.TempData["AddBuyers"] = $"Фирмата вече съществува";
                return this.View(addBuyerViewModel);
            }

            return this.Redirect("/Buyer/All");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var userId = this.userManager.GetUserId(this.User);

            var user = this.dataBaseOwnerRepository.All().Select(x => x.ApplicationUsers.FirstOrDefault(u => u.Id == userId)).FirstOrDefault();

            var dbOwner = this.dataBaseOwnerRepository.All().FirstOrDefault(d => d.Id == user.DatabaseОwnerId);

            var buyers = await this.buyerService.All(dbOwner.Id);

            return this.View("All", buyers);
        }
    }
}
