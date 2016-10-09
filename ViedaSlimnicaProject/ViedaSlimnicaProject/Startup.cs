using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ViedaSlimnicaProject.Startup))]
namespace ViedaSlimnicaProject
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
