//----------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationEngine.cs" owner="Sercan Timoçin" namespace="Ottomon.Core">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 20:30</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottomon.Core
{
    using System.Web.Http;

    using Ottoman.Injector;
    using Ottoman.Mapper;

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
        /// The ınitialize.
        /// </summary>
        /// <param name="configuration"></param>
        public static void Initialize(HttpConfiguration configuration)
        {
            _httpConfiguration = configuration;

            SimpleInjectorManager.Initialize(Container, configuration);
        }

        /// <summary>
        /// The ınitialize.
        /// </summary>
        /// <param name="configuration">
        /// Need global httpConfiguration 
        /// The configuration.
        /// </param>
        /// <param name="projectName">
        /// The project name has models which will map
        /// </param>
        public static void Initialize(HttpConfiguration configuration, string projectName)
        {
            Initialize(configuration);

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
