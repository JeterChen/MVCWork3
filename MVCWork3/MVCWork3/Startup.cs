using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCWork3.Startup))]
namespace MVCWork3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
