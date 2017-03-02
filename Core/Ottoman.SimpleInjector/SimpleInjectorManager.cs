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
    using System.Web.Mvc;

    using Ottoman.Injector.Policies.Controller;
    using Ottoman.Injector.Policies.Repository;

    using SimpleInjector;
    using SimpleInjector.Advanced;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using SimpleInjector.Integration.WebApi;

    /// <summary>
    /// The simple ınjector manager.
    /// </summary>
    public static class SimpleInjectorManager
    {
        /// <summary>
        /// The is call initialize.
        /// </summary>
        private static bool isCallInitialize = false;

        /// <summary>
        /// The MVC initialize.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="assemblies">
        /// The assemblies.
        /// </param>
        /// <param name="dependencyResolver">
        /// The dependency resolver.
        /// </param>
        public static void MvcInitialize(Container container, Assembly[] assemblies, IDependencyResolver dependencyResolver)
        {
            if (!isCallInitialize)
            {
                container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
                container.Options.LifestyleSelectionBehavior = new MvcInjectionLifestyle();

                RegisterController(container, null, assemblies);
                RegisterInstaller(container);

                container.Verify();
                DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

                isCallInitialize = true;
            }
        }

        /// <summary>
        /// The web API initialize.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="httpConfiguration">
        /// The http configuration.
        /// </param>
        public static void WebApiInitialize(Container container, HttpConfiguration httpConfiguration)
        {
            if (!isCallInitialize)
            {
                container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
                container.Options.LifestyleSelectionBehavior = new WebApiInjectionLifestyle();

                RegisterController(container, httpConfiguration, null);
                RegisterInstaller(container);

                container.Verify();

                httpConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
                isCallInitialize = true;
            }
        }

        /// <summary>
        /// The non web initialize.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public static void NonWebInitialize(Container container)
        {
            if (!isCallInitialize)
            {
                RegisterInstaller(container);

                container.Verify();

                isCallInitialize = true;
            }
        }

        /// <summary>
        /// The register all.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        private static void RegisterInstaller(Container container)
        {
            var autoInstallerClases = Assembly.GetCallingAssembly().GetExportedTypes().Where(x => typeof(IRepositoryInstaller).IsAssignableFrom(x) && x.IsClass);

            foreach (var autoInstallerClass in autoInstallerClases)
            {
                IRepositoryInstaller repositoryInstaller = Activator.CreateInstance(autoInstallerClass) as IRepositoryInstaller;

                repositoryInstaller.Register(container);
            }
        }

        /// <summary>
        /// The register controller.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        /// <param name="assemblies">
        /// The assemblies.
        /// </param>
        private static void RegisterController(Container container, HttpConfiguration configuration, Assembly[] assemblies)
        {
            var controllerInstallerClasesTypes = Assembly.GetCallingAssembly().GetExportedTypes().Where(x => typeof(IControllerInstaller).IsAssignableFrom(x) && x.IsClass);

            foreach (var controllerInstallerClassType in controllerInstallerClasesTypes)
            {
                IControllerInstaller controllerInstaller = Activator.CreateInstance(controllerInstallerClassType) as IControllerInstaller;

                controllerInstaller.Register(container, configuration, assemblies);
            }
        }
    }

    /// <summary>
    /// The web API lifestyle.
    /// </summary>
    internal class WebApiInjectionLifestyle : ILifestyleSelectionBehavior
    {
        /// <summary>
        /// The select lifestyle.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>s
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

    /// <summary>
    /// The MVC injection lifestyle.
    /// </summary>
    internal class MvcInjectionLifestyle : ILifestyleSelectionBehavior
    {
        /// <summary>
        /// The select lifestyle.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>s
        /// <param name="implementationType">
        /// The implementation type.
        /// </param>
        /// <returns>
        /// The <see cref="Lifestyle"/>.
        /// </returns>
        public Lifestyle SelectLifestyle(Type serviceType, Type implementationType)
        {
            return WebRequestLifestyle.Scoped;
        }
    }
}
