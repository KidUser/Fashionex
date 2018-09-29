using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrossoverShoppingCart.Startup))]
namespace CrossoverShoppingCart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
