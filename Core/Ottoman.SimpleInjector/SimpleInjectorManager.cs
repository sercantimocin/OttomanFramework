//----------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleInjectorManager.cs" company="Doğuş Bilgi İşlem ve Teknoloji Hizmetleri A.Ş." namespace="Ottoman.SimpleInjector">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 20:38</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottoman.Injector
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;

    using Policies;

    using SimpleInjector;
    using SimpleInjector.Advanced;
    using SimpleInjector.Integration.WebApi;

    /// <summary>
    /// The simple ınjector manager.
    /// </summary>
    public static class SimpleInjectorManager
    {
        /// <summary>
        /// The is call ınitiliaze.
        /// </summary>
        private static bool isCallInitiliaze = false;

        /// <summary>
        /// The ınitialize.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="httpConfiguration">
        /// The http configuration.
        /// </param>
        public static void Initialize(Container container, HttpConfiguration httpConfiguration)
        {
            if (!isCallInitiliaze)
            {
                container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle(true);

                container.Options.LifestyleSelectionBehavior = new WebApiLifestyle();

                RegisterAll(container, httpConfiguration);

                container.Verify();

                httpConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

                isCallInitiliaze = true;
            }
        }

        /// <summary>
        /// The register all.
        /// </summary>
        private static void RegisterAll(Container container, HttpConfiguration httpConfiguration)
        {
            var autoInstallerClases = Assembly.GetCallingAssembly().GetExportedTypes().Where(x => typeof(IInstaller).IsAssignableFrom(x) && x.IsClass);

            foreach (var autoInstallerClass in autoInstallerClases)
            {
                IInstaller installer = Activator.CreateInstance(autoInstallerClass) as IInstaller;

                installer.Register(container, httpConfiguration);
            }
        }
    }

    /// <summary>
    /// The web api lifestyle.
    /// </summary>
    internal class WebApiLifestyle : ILifestyleSelectionBehavior
    {
        /// <summary>
        /// The select lifestyle.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <param name="implementationType">
        /// The implementation type.
        /// </param>
        /// <returns>
        /// The <see cref="Lifestyle"/>.
        /// </returns>
        public Lifestyle SelectLifestyle(Type serviceType, Type implementationType)
        {
            return WebApiRequestLifestyle.Scoped;
        }
    }
}
