using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VendorManagment.Startup))]
namespace VendorManagment
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
