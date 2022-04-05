namespace InvoiceAndStorage.Web.Controllers
{
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using InvoiceAndStorage.Web.ViewModels.Supplier;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SupplierController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDataBaseOwnerService databaseOwner;
        private readonly ISupplierSevice supplierSevice;

        public SupplierController(
            UserManager<ApplicationUser> userManager,
            IDataBaseOwnerService databaseOwner,
            ISupplierSevice supplierSevice)
        {
            this.userManager = userManager;
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

            var owner = await this.databaseOwner.GetDatabaseОwner(user);

            var isCreate = await this.supplierSevice.CreateSupplire(supplierViewModel, user, owner);

            if (!isCreate)
            {
                this.TempData["AddSuppliers"] = $"Фирмата вече съществува";
                return this.View(supplierViewModel);
            }

            return this.View();
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
