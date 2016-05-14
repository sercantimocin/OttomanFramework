//----------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerInstaller.cs" owner="Sercan Timoçin" namespace="Ottoman.SimpleInjector">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 22:10</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottoman.SimpleInjector.Auto.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;

    using global::SimpleInjector;

    /// <summary>
    /// The controller ınstaller.
    /// </summary>
    public class ControllerInstaller : IInstaller
    {
        /// <summary>
        /// The web api project name.
        /// </summary>
        private const string WebApiProjectName = "WebApiProjectName";

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public void Register(Container container)
        {
            string webApiProjectName = ConfigurationManager.AppSettings[WebApiProjectName];

            var apiAssembly = Assembly.Load(webApiProjectName);

            if (apiAssembly != null)
            {
                IEnumerable<Type> apiClasesTypes = apiAssembly.ExportedTypes.Where(x => typeof(ApiController).IsAssignableFrom(x) && x.IsClass);

                foreach (var apiClasesType in apiClasesTypes)
                {
                    container.Register(typeof(ApiController), apiClasesType, Lifestyle.Scoped);
                    //TODO Research RegisterWebApiControllers 
                }
            }
        }
    }
}
