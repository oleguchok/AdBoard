using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdBoard.WebUI.Startup))]
namespace AdBoard.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
