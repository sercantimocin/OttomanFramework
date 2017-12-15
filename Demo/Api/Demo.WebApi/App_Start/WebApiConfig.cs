//----------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" owner="Sercan Timoçin" namespace="Demo.WebApi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>devpc</author>
// <date>2016-5-17 22:34</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Demo.WebApi
{
    using System;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;

    using Newtonsoft.Json;

    using Ottoman.CrossCutting.Additional;

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
                defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new OttomanHandler("1.0.0"));
            config.Services.Replace(typeof(IExceptionHandler), new OttomanExceptionHandler(config.Services.GetExceptionHandler()));
            config.Formatters.Add(new BrowserJsonFormatter());
        }
    }

    /// <summary>
    /// The browser json formatter.
    /// </summary>
    public class BrowserJsonFormatter : JsonMediaTypeFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserJsonFormatter"/> class.
        /// </summary>
        public BrowserJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            this.SerializerSettings.Formatting = Formatting.Indented;
        }

        /// <summary>
        /// The set default content headers.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="headers">
        /// The headers.
        /// </param>
        /// <param name="mediaType">
        /// The media type.
        /// </param>
        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}
