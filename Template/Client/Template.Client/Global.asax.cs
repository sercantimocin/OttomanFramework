using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Template.Client
{
    using System.Reflection;
    using System.Web.Http;

    using Ottoman.Core;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ApplicationEngine.MvcInitialize(GlobalConfiguration.Configuration, "Template.Client");
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
