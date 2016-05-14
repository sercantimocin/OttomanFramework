//----------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleInjectorManager.cs" company="Doğuş Bilgi İşlem ve Teknoloji Hizmetleri A.Ş." namespace="Ottoman.SimpleInjector">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 20:38</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottoman.SimpleInjector
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    using Ottoman.SimpleInjector.Auto;

    using Si = global::SimpleInjector;

    /// <summary>
    /// The simple ınjector manager.
    /// </summary>
    public class SimpleInjectorManager
    {
        /// <summary>
        /// The _container.
        /// </summary>
        public static readonly Si.Container Container = new Si.Container();

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInjectorManager"/> class.
        /// </summary>
        public SimpleInjectorManager()
        {
            Container.Options.DefaultScopedLifestyle = new Si.Integration.WebApi.WebApiRequestLifestyle();

            this.RegisterAll();

            Container.Verify();

            //DependencyResolver.SetResolver(Si.SimpleInjectorDependencyResolver(container));
        }

        /// <summary>
        /// The register all.
        /// </summary>
        private void RegisterAll()
        {
            var autoInstallerClases = Assembly.GetCallingAssembly().GetExportedTypes().Where(x => typeof(IInstaller).IsAssignableFrom(x) && x.IsClass);

            foreach (var autoInstallerClass in autoInstallerClases)
            {
                var installer = Activator.CreateInstance(autoInstallerClass) as IInstaller;

                installer.Register(Container);
            }
        }
    }
}
