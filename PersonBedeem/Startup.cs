using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonBedeem.Startup))]
namespace PersonBedeem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
