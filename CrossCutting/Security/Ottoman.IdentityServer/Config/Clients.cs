namespace Ottoman.IdentityServer.Config
{
    using System.Collections.Generic;

    using IdentityServer3.Core.Models;

    using IdentityServer;

    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
             {
                new Client
                {
                     ClientId = "Flow1ClientCredential",
                     ClientName = "Ottoman Client Credential",
                     Flow = Flows.ClientCredentials,
                     AllowAccessToAllScopes = true,

                    ClientSecrets = new List<Secret>()
                    {
                        new Secret(Constants.ClientSecret.Sha256())
                    }
                }
                //,
                //new Client
                //{
                //     ClientId = "tripgalleryauthcode",
                //     ClientName = "Trip Gallery (Authorization Code)",
                //     Flow = Flows.AuthorizationCode,
                //     AllowAccessToAllScopes = true,

                //    // redirect = URI of the MVC application callback
                //    RedirectUris = new List<string>
                //    {
                //        Constants.TripGalleryMVCSTSCallback
                //    },           

                //    // client secret
                //    ClientSecrets = new List<Secret>()
                //    {
                //        new Secret(Constants.TripGalleryClientSecret.Sha256())
                //    }
                //}
                //,
                //new Client
                //{
                //     ClientId = "tripgalleryimplicit",
                //     ClientName = "Trip Gallery (Implicit)",
                //     Flow = Flows.Implicit,
                //     AllowAccessToAllScopes = true,

                //    // redirect = URI of the Angular application callback page
                //    RedirectUris = new List<string>
                //    {
                //        Constants.TripGalleryAngular + "callback.html"
                //    }
                //}
                //,
                //new Client
                //{
                //     ClientId = "tripgalleryropc",
                //     ClientName = "Trip Gallery (Resource Owner Password Credentials)",
                //     Flow = Flows.ResourceOwner,
                //     AllowAccessToAllScopes = true,

                //    ClientSecrets = new List<Secret>()
                //    {
                //        new Secret(Constants.TripGalleryClientSecret.Sha256())
                //    }
                //}

             };
        }
    }
}
