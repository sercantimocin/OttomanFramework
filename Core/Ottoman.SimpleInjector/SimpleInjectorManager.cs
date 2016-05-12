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
        private static Si.Container _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInjectorManager"/> class.
        /// </summary>
        public SimpleInjectorManager()
        {
            _container = new Si.Container();

            _container.Verify();

            //DependencyResolver.SetResolver(Si.SimpleInjectorDependencyResolver(container));

            this.RegisterAll();
        }

        /// <summary>
        /// The register all.
        /// </summary>
        private void RegisterAll()
        {
            var applicationAssembly = AppDomain.CurrentDomain.GetAssemblies();

            var autoInstallerClases = Assembly.GetCallingAssembly().GetExportedTypes().Where(x => typeof(Installer).IsAssignableFrom(x) && x.IsClass);

            foreach (var autoInstallerClass in autoInstallerClases)
            {
                var installer = Activator.CreateInstance(autoInstallerClass) as Installer;

                if (installer != null)
                {
                    installer.Register(applicationAssembly, _container);
                }
            }
        }
    }
}
