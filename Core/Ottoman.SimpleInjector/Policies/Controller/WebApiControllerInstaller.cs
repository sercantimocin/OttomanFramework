//----------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerInstaller.cs" owner="Sercan Timoçin" namespace="Ottoman.SimpleInjector">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 22:10</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottoman.Injector.Policies.Controller
{
    using System.Web.Http;

    using SimpleInjector;

    using Container = SimpleInjector.Container;

    /// <summary>
    /// The controller ınstaller.
    /// </summary>
    public class WebApiControllerInstaller : IControllerInstaller
    {
        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="httpConfiguration">
        /// Http Configuration
        /// </param>
        /// <param name="assemblies">
        /// The assemblies.
        /// </param>
        public void Register(Container container, HttpConfiguration httpConfiguration)
        {
            container.RegisterWebApiControllers(httpConfiguration);
        }
    }
}
