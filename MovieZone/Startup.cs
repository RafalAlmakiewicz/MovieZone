using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieZone.Startup))]
namespace MovieZone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
