namespace InvoiceAndStorage.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Common.Repositories;
    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly IValidViewModelsService validViewModelsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<DatabaseОwner> dataBaseOwnerRepository;
        private readonly IDataBaseOwnerService dataBaseOwnerService;
        private readonly IDeletableEntityRepository<ApplicationUser> applicationUserRepository;

        public ProductController(
            IProductService productService,
            IValidViewModelsService validViewModelsService,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<DatabaseОwner> dataBaseOwnerRepository,
            IDataBaseOwnerService dataBaseOwnerService,
            IDeletableEntityRepository<ApplicationUser> applicationUserRepository)
        {
            this.productService = productService;
            this.validViewModelsService = validViewModelsService;
            this.userManager = userManager;
            this.dataBaseOwnerRepository = dataBaseOwnerRepository;
            this.dataBaseOwnerService = dataBaseOwnerService;
            this.applicationUserRepository = applicationUserRepository;
        }

        public IActionResult AddProduct()
        {
            var supplier = this.HttpContext.Request.RouteValues.Values.ToList();

            if (supplier.Count < 3)
            {
                return this.RedirectToAction("AddProductWithoutVatNumber");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel addProductViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(addProductViewModel);
            }

            var user = this.userManager.GetUserId(this.User);
            var ownerId = await this.dataBaseOwnerService.GetDatabaseОwner(user);

            var (isValid, error) = this.validViewModelsService.IsValidProductModel(addProductViewModel, ownerId);

            if (!isValid)
            {
                this.TempData["AddProduct"] = error;
                return this.View(addProductViewModel);
            }

            var supplier = this.HttpContext.Request.RouteValues.Values.ToList();

            string companyIdentificationNumber = supplier[2].ToString();

            var isCreated = await this.productService.CreateProduct(addProductViewModel, companyIdentificationNumber, ownerId);

            if (!isCreated)
            {
                this.TempData["AddProduct"] = $"Продукт с такова име вече съществува";
                return this.View(addProductViewModel);
            }

            return this.RedirectToAction("AllProducts");
        }

        [HttpGet]
        public IActionResult AddProductWithoutVatNumber() => this.View();

        [HttpPost]
        public async Task<IActionResult> AddProductWithoutVatNumber(AddProductWithoutVatNumberViewModel addProductViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(addProductViewModel);
            }

            var user = this.userManager.GetUserId(this.User);
            var ownerId = await this.dataBaseOwnerService.GetDatabaseОwner(user);

            var (isValid, error) = this.validViewModelsService.IsValidProductWithoutVatNumberModel(addProductViewModel, ownerId);

            if (!isValid)
            {
                this.TempData["AddProduct"] = error;
                return this.View(addProductViewModel);
            }

            var isCreated = await this.productService.CreateProduct(addProductViewModel, addProductViewModel.CompanyIdentificationNumber);

            if (!isCreated)
            {
                this.TempData["AddProductWithoutVatNumber"] = $"Продуkт с такова име вече съществува";
                return this.View(addProductViewModel);
            }

            return this.RedirectToAction("AllProducts");
        }

        public async Task<IActionResult> AllProducts()
        {
            var userId = this.userManager.GetUserId(this.User);

            var user = this.applicationUserRepository.All().FirstOrDefault(u => u.Id == userId);

            var dbOwner = this.dataBaseOwnerRepository.All().FirstOrDefault(d => d.Id == user.DatabaseОwnerId);

            var allProducts = await this.productService.GetAllProducts(dbOwner);

            return this.View(allProducts);
        }
    }
}
