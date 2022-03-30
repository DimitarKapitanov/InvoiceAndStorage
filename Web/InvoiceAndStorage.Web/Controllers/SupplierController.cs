namespace InvoiceAndStorage.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class SupplierController : BaseController
    {
        public SupplierController()
        {
        }

        public async Task<IActionResult> AddSuppliers()
        {
            return this.View();
        }
    }
}
