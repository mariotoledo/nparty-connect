using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdminConnect.Startup))]
namespace AdminConnect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
