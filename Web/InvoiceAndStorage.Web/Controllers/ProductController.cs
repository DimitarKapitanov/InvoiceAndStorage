namespace InvoiceAndStorage.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class ProductController : BaseController
    {
        public ProductController()
        {
        }

        public async Task<IActionResult> CreateProduct()
        {
            return this.View();
        }
    }
}
