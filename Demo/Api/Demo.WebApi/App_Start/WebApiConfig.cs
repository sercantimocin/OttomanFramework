//----------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" owner="Sercan Timoçin" namespace="Template.WebApi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>devpc</author>
// <date>2016-5-17 22:34</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Demo.WebApi
{
    using System.Web.Http;

    using Microsoft.Owin.Security.OAuth;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
