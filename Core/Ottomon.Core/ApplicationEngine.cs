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

    using SimpleInjector;

    /// <summary>
    /// The application engine.
    /// </summary>
    public static class ApplicationEngine
    {
        private static SimpleInjectorManager _injectorManager = null;

        private static HttpConfiguration _httpConfiguration;

        /// <summary>
        /// The ınitialize.
        /// </summary>
        /// <param name="configuration"></param>
        public static void Initialize(HttpConfiguration configuration)
        {
            _httpConfiguration = configuration;
            _injectorManager = new SimpleInjectorManager(configuration);

            //Container = _injectorManager.Container;
        }

        public static Container Container
        {
            get
            {
                if (_injectorManager != null && _injectorManager.Container != null)
                {
                    return _injectorManager.Container;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
