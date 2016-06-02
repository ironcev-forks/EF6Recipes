using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UsingEntityFrameworkInMVC.Startup))]
namespace UsingEntityFrameworkInMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
