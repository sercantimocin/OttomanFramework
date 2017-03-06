//----------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationEngine.cs" owner="Sercan Timoçin" namespace="Ottomon.Core">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 20:30</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottoman.Core
{
    using System.Web.Http;

    using Injector;
    using Mapper;

    using SimpleInjector;

    /// <summary>
    /// The application engine.
    /// </summary>
    public static class ApplicationEngine
    {
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

        public static void MvcInitialize(HttpConfiguration httpConfiguration, string projectName)
        {
            SimpleInjectorManager.MvcInitialize(Container, httpConfiguration);
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
