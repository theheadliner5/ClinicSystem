using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClinicSystem.WebApplication.Startup))]
namespace ClinicSystem.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
