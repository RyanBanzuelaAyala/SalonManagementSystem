using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eApp.Web.Admin.Startup))]
namespace eApp.Web.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
