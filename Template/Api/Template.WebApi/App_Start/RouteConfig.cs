//----------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" owner="Sercan Timoçin" namespace="Template.WebApi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>devpc</author>
// <date>2016-5-17 22:34</date>
//---------------------------------------------------------------------------------------------------------------------- 

using System.Web.Mvc;
using System.Web.Routing;

namespace Template.WebApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
