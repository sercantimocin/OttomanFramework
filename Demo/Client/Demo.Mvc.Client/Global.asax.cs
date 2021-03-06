﻿namespace Demo.Mvc.Client
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Ottoman.Core;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ApplicationEngine.MvcInitialize(GlobalConfiguration.Configuration, "Demo.Mvc.Client");
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
