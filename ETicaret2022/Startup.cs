using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ETicaret2022.Startup))]
namespace ETicaret2022
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
