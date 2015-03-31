using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdBoard.Startup))]
namespace AdBoard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
