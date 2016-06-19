using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Template.WebApi.Startup))]

namespace Template.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
