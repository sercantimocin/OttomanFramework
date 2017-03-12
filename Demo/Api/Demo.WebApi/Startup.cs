using Demo.WebApi;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Demo.WebApi
{
    using IdentityServer3.AccessTokenValidation;

    using Ottoman.IdentityServer;

    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            //app.UseIdentityServerBearerTokenAuthentication(
            // new IdentityServerBearerTokenAuthenticationOptions
            // {
            //     Authority = Constants.EndPoint,
            //     //RequiredScopes = new[] { "gallerymanagement" }
            //     RequiredScopes = new[] { "Test" },

            // });
        }
    }
}
