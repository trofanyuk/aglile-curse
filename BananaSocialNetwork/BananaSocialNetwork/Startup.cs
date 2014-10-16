using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BananaSocialNetwork.Startup))]
namespace BananaSocialNetwork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
