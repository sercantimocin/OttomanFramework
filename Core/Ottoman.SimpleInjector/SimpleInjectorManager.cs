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

    using Policies;

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
        /// The is call ınitiliaze.
        /// </summary>
        private static bool isCallInitiliaze = false;

        /// <summary>
        /// The ınitialize.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="assemblies">Mvc Assembly</param>
        /// <param name="dependencyResolver">Mvc project depencency resolver</param>
        public static void MvcInitialize(Container container, Assembly[] assemblies, IDependencyResolver dependencyResolver)
        {
            if (!isCallInitiliaze)
            {
                container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

                container.Options.LifestyleSelectionBehavior = new MvcInjectionLifestyle();

                MvcControllerInstaller controllerInstaller = new MvcControllerInstaller();

                controllerInstaller.Register(container, assemblies);

                RegisterAll(container);

                container.Verify();

                DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

                isCallInitiliaze = true;
            }
        }

        /// <summary>
        /// The ınitialize.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="httpConfiguration">
        /// The http configuration.
        /// </param>
        public static void WebApiInitialize(Container container, HttpConfiguration httpConfiguration)
        {
            if (!isCallInitiliaze)
            {
                container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

                container.Options.LifestyleSelectionBehavior = new WebApiInjectionLifestyle();

                WebApiControllerInstaller controllerInstaller = new WebApiControllerInstaller();

                controllerInstaller.Register(container, httpConfiguration);

                RegisterAll(container);

                container.Verify();

                httpConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

                isCallInitiliaze = true;
            }
        }

        /// <summary>
        /// The register all.
        /// </summary>
        private static void RegisterAll(Container container)
        {
            var autoInstallerClases = Assembly.GetCallingAssembly().GetExportedTypes().Where(x => typeof(IInstaller).IsAssignableFrom(x) && x.IsClass);

            foreach (var autoInstallerClass in autoInstallerClases)
            {
                IInstaller installer = Activator.CreateInstance(autoInstallerClass) as IInstaller;

                installer.Register(container);
            }
        }
    }

    /// <summary>
    /// The web api lifestyle.
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
            //return Lifestyle.Scoped;
            return WebApiRequestLifestyle.Scoped;

        }
    }


    /// <summary>
    /// The web api lifestyle.
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
