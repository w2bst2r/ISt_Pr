using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StajProject.Startup))]
namespace StajProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
