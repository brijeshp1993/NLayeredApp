using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Demo.Web.MVC.Startup))]
namespace Demo.Web.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
