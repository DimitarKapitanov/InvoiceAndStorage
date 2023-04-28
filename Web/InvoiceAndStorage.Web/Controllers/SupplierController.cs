namespace InvoiceAndStorage.Web.Controllers
{
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Supplier;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class SupplierController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IValidViewModelsService validViewModelsService;
        private readonly IDataBaseOwnerService databaseOwner;
        private readonly ISupplierService supplierSevice;

        public SupplierController(
            UserManager<ApplicationUser> userManager,
            IValidViewModelsService validViewModelsService,
            IDataBaseOwnerService databaseOwner,
            ISupplierService supplierSevice)
        {
            this.userManager = userManager;
            this.validViewModelsService = validViewModelsService;
            this.databaseOwner = databaseOwner;
            this.supplierSevice = supplierSevice;
        }

        public IActionResult AddSuppliers() => this.View();

        [HttpPost]
        public async Task<IActionResult> AddSuppliers(AddSupplierViewModel supplierViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(supplierViewModel);
            }

            var user = this.userManager.GetUserId(this.User);
            var ownerId = await this.databaseOwner.GetDatabaseОwner(user);

            var (isValid, error) = this.validViewModelsService.IsValidSupplierModel(supplierViewModel, ownerId);

            if (!isValid)
            {
                this.TempData["AddSuppliers"] = error;
                return this.View(supplierViewModel);
            }

            var isCreate = await this.supplierSevice.CreateSupplire(supplierViewModel, user, ownerId);

            if (!isCreate)
            {
                this.TempData["AddSuppliers"] = $"Фирмата вече съществува";
                return this.View(supplierViewModel);
            }

            return this.Redirect("/Product/AddProduct");
        }

        public async Task<IActionResult> AllSuppliers()
        {
            var userId = this.userManager.GetUserId(this.User);

            var dbOwnerId = await this.databaseOwner.GetDatabaseОwner(userId);

            var supplires = await this.supplierSevice.AllSuppliers(dbOwnerId);

            return this.View("AllSuppliers", supplires);
        }
    }
}
