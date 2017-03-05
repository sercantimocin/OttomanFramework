namespace Ottoman.IdentityServer
{
    using System;
    using System.Diagnostics;
    using System.Security.Cryptography.X509Certificates;

    using IdentityServer3.Core.Configuration;
    using IdentityServer3.Core.Services.Default;

    using Ottoman.IdentityServer.Config;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        { 
            app.Map("/identity", idsrvApp =>
            {

                var corsPolicyService = new DefaultCorsPolicyService()
                {
                    AllowAll = true
                };

                var idServerServiceFactory = new IdentityServerServiceFactory()
                                .UseInMemoryClients(Clients.Get())
                                .UseInMemoryScopes(Scopes.Get())
                                .UseInMemoryUsers(Users.Get());


                idServerServiceFactory.CorsPolicyService = new
                    Registration<IdentityServer3.Core.Services.ICorsPolicyService>(corsPolicyService);

                var options = new IdentityServerOptions
                {
                    Factory = idServerServiceFactory,
                    SiteName = "Ottoman Security Token Service",                    
                    IssuerUri = Constants.IssuerUri,
                    PublicOrigin = Constants.Origin,
                    RequireSsl = false,
                    EnableWelcomePage = true
                    ////SigningCertificate = this.LoadCertificate()
                };

                idsrvApp.UseIdentityServer(options);
            });
        }


        X509Certificate2 LoadCertificate()
        {
            Debug.WriteLine("*********");
            Debug.WriteLine(string.Format(@"{0}Certificates\mycert2.pfx",
                AppDomain.CurrentDomain.BaseDirectory));

            return new X509Certificate2(
                string.Format(@"{0}Certificates\idsrv3test.pfx",
                AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}
