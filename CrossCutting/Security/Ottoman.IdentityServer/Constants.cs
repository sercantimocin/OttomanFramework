
namespace Ottoman.IdentityServer
{
    public class Constants
    {

        //public const string TripGalleryAPI = "https://localhost:44315/";
        //public const string TripGalleryMVC = "https://localhost:44318/";
        //public const string TripGalleryMVCSTSCallback = "https://localhost:44318/stscallback";
        //public const string TripGalleryAngular = "https://localhost:44316/";

        public const string ClientSecret = "myrandomclientsecret";

        public const string IssuerUri = "https://ottomanframework/identity";
        public const string Origin = "http://localhost:20592/";

        public const string EndPoint = Origin + "/identity";
        public const string TokenEndpoint = EndPoint + "/connect/token";
        public const string AuthorizationEndpoint = EndPoint + "/connect/authorize";
        public const string UserInfoEndpoint = EndPoint + "/connect/userinfo";
        public const string EndSessionEndpoint = EndPoint + "/connect/endsession";
        public const string RevokeTokenEndpoint = EndPoint + "/connect/revocation";
    }

}
