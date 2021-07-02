using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LAB04.Startup))]
namespace LAB04
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
