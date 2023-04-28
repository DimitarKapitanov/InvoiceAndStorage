namespace InvoiceAndStorage.Web.Controllers
{
    using InvoiceAndStorage.Services.Data.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class InquiryController : BaseController
    {
        private readonly IInvoiceService invoiceService;

        public InquiryController()
        {
        }

        [HttpGet]
        public IActionResult Inquiry()
        {
            return this.View();
        }
    }
}
