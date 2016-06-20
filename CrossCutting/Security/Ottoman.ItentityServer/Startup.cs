namespace Ottoman.ItentityServer
{
    using System;
    using System.Security.Cryptography.X509Certificates;

    using IdentityServer3.Core.Configuration;

    using Config;

    using Owin;


    public class Startup
    {
        public void Configuration(IAppBuilder app)
        { 
            app.Map("/identity", idsrvApp =>
            {
                var idServerServiceFactory = new IdentityServerServiceFactory()
                                .UseInMemoryClients(Clients.Get())
                                .UseInMemoryScopes(Scopes.Get())
                                .UseInMemoryUsers(Users.Get());

                var options = new IdentityServerOptions
                {
                    Factory = idServerServiceFactory,
                    SiteName = "Ottoman Security Token Service",                    
                    IssuerUri = Constants.TripGalleryIssuerUri,
                    PublicOrigin = Constants.TripGallerySTSOrigin,
                    SigningCertificate = this.LoadCertificate()
                };

                idsrvApp.UseIdentityServer(options);
            });
        }


        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\certificates\idsrv3test.pfx",
                AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}
