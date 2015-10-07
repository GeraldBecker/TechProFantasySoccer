using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TechProFantasySoccer.Startup))]
namespace TechProFantasySoccer
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
