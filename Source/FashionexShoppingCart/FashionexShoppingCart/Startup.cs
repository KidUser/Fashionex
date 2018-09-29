using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FashinexShoppingCart.Startup))]
namespace FashinexShoppingCart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
