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

        public static void MvcInitialize(Assembly[] assemblies, IDependencyResolver dependencyResolver, string projectName)
        {
            SimpleInjectorManager.MvcInitialize(Container, assemblies, dependencyResolver);
            Initialize(projectName);
        }

        /// <summary>
        /// The ınitialize.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="projectName"></param>
        public static void WebApiInitialize(HttpConfiguration configuration, string projectName)
        {
            _httpConfiguration = configuration;

            SimpleInjectorManager.WebApiInitialize(Container, configuration);
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

        /// <summary>
        /// Gets the container.
        /// </summary>
        public static Container Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new Container();
                }

                return _container;
            }
        }
    }
}
