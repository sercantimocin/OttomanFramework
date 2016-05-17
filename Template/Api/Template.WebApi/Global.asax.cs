namespace Template.WebApi
{
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Ottomon.Core;

    /// <summary>
    /// The web api application.
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// The application_ start.
        /// </summary>
        protected void Application_Start()
        {
            ApplicationEngine.Initialize();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
