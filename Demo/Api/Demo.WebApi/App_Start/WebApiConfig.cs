//----------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" owner="Sercan Timoçin" namespace="Demo.WebApi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>sercan timocin</author>
// <date>2016-5-17 22:34</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Demo.WebApi
{
    using System;
    using System.Net;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;

    using FluentValidation.WebApi;

    using Newtonsoft.Json;

    using Ottoman.Core.Infrastructure;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });


            config.MessageHandlers.Add(new OttomanHandler("1.0.0", true));
            config.Services.Replace(typeof(IExceptionHandler), new OttomanExceptionHandler(config.Services.GetExceptionHandler()));
            config.Formatters.Add(new OttomanBrowserJsonFormatter());

            config.Filters.Add(new OttomanApiValidationFilter(HttpStatusCode.OK));
            FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}
