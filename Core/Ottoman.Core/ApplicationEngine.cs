//----------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationEngine.cs" owner="Sercan Timoçin" namespace="Ottomon.Core">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 20:30</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottoman.Core
{
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;

    using Injector;
    using Mapper;

    using SimpleInjector;
    
    /// <summary>
    /// The application engine.
    /// </summary>
    public static class ApplicationEngine
    {
        /// <summary>
        /// The _http configuration.
        /// </summary>
        private static HttpConfiguration _httpConfiguration;

        /// <summary>
        /// The Injector Container.
        /// </summary>
        private static Container _container;

        /// <summary>
        /// Gets the container.
        /// </summary>
        public static Container Container
        {
            get
            {
                return _container ?? (_container = new Container());
            }
        }

        /// <summary>
        /// The MVC initialize.
        /// </summary>
        /// <param name="assemblies">
        /// The assemblies.
        /// </param>
        /// <param name="dependencyResolver">
        /// The dependency resolver.
        /// </param>
        /// <param name="projectName">
        /// The project name.
        /// </param>
        public static void MvcInitialize(Assembly[] assemblies, IDependencyResolver dependencyResolver, string projectName)
        {
            SimpleInjectorManager.MvcInitialize(Container, assemblies, dependencyResolver);
            Initialize(projectName);
        }

        /// <summary>
        /// The web API initialize.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        /// <param name="projectName">
        /// The project name.
        /// </param>
        public static void WebApiInitialize(HttpConfiguration configuration, string projectName)
        {
            _httpConfiguration = configuration;

            SimpleInjectorManager.WebApiInitialize(Container, configuration);
            Initialize(projectName);
        }

        /// <summary>
        /// The non web initialize.
        /// </summary>
        /// <param name="projectName">
        /// The project Name.
        /// </param>
        public static void NonWebInitialize(string projectName)
        {
            SimpleInjectorManager.NonWebInitialize(Container);
            Initialize(projectName);
        }

        /// <summary>
        /// The ınitialize.
        /// </summary>
        /// <param name="projectName">
        /// The project name has models which will map
        /// </param>
        private static void Initialize(string projectName)
        {
            AutoMapperManager.RegisterClassesBulk(projectName);
        }
    }
}
