//----------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerInstaller.cs" owner="Sercan Timoçin" namespace="Ottoman.SimpleInjector">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 22:10</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottoman.Injector.Policies.Controller
{
    using System.Configuration;
    using System.Web.Http;

    using global::SimpleInjector;

    using Policies;

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
        ///     The container.
        /// </param>
        /// <param name="httpConfiguration"></param>
        public void Register(Container container, HttpConfiguration httpConfiguration)
        {
            container.RegisterWebApiControllers(httpConfiguration);

            string webApiProjectName = ConfigurationManager.AppSettings[WebApiProjectName];

            //var apiAssembly = Assembly.Load(webApiProjectName);

            //if (apiAssembly != null)
            //{
            //    IEnumerable<Type> apiClasesTypes = apiAssembly.ExportedTypes.Where(x => typeof(ApiController).IsAssignableFrom(x) && x.IsClass);

            //    foreach (var apiClasesType in apiClasesTypes)
            //    {
            //        container.Register(typeof(ApiController), apiClasesType, Lifestyle.Scoped);
            //        //TODO Research RegisterWebApiControllers 
            //    }
            //}
        }
    }
}
