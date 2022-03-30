using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(InvoiceAndStorage.Web.Areas.Identity.IdentityHostingStartup))]

namespace InvoiceAndStorage.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}
