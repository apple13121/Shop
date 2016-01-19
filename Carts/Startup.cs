using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Carts.Startup))]
namespace Carts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
