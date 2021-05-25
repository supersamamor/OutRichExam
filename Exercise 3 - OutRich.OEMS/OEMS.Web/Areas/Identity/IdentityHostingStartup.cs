using Microsoft.AspNetCore.Hosting;
[assembly: HostingStartup(typeof(OEMS.Web.Areas.Identity.IdentityHostingStartup))]
namespace OEMS.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}