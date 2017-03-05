using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(Template.WebApi.Startup))]

namespace Template.WebApi
{
    using System;
    using System.Security.Cryptography.X509Certificates;

    using IdentityServer3.AccessTokenValidation;

    using Ottoman.IdentityServer;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.UseIdentityServerBearerTokenAuthentication(
             new IdentityServerBearerTokenAuthenticationOptions
             {
                 Authority = Constants.EndPoint,
                 //RequiredScopes = new[] { "gallerymanagement" }
                 RequiredScopes = new[] { "Test" },

             });
        }
    }
}
