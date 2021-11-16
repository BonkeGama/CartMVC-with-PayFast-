using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CartMvc.Startup))]
namespace CartMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
