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

    using Repository.Pattern.DataContext;
    using Repository.Pattern.UnitOfWork;

    using SimpleInjector;
    using SimpleInjector.Advanced;
    using SimpleInjector.Diagnostics;
    using SimpleInjector.Integration.WebApi;

    /// <summary>
    /// The simple ınjector manager.
    /// </summary>
    public class SimpleInjectorManager
    {
        /// <summary>
        /// The _container.
        /// </summary>
        public readonly Container Container = new Container();

        private readonly HttpConfiguration _httpConfiguration = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInjectorManager"/> class.
        /// </summary>
        public SimpleInjectorManager(HttpConfiguration httpConfiguration)
        {
            this._httpConfiguration = httpConfiguration;

            this.Container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle(true);

            this.Container.Options.LifestyleSelectionBehavior = new WebApiLifestyle();

            this.RegisterAll();

            //this.SuppressRepositoryInstallerWarnings();

            this.Container.Verify();

            httpConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(Container);
        }

        //private void SuppressRepositoryInstallerWarnings()
        //{
        //    this.SuppressWarning(typeof(IDataContext),DiagnosticType.DisposableTransientComponent);
        //    this.SuppressWarning(typeof(IDataContextAsync),DiagnosticType.DisposableTransientComponent);
        //    this.SuppressWarning(typeof(IUnitOfWork),DiagnosticType.DisposableTransientComponent);
        //    this.SuppressWarning(typeof(IUnitOfWorkAsync),DiagnosticType.DisposableTransientComponent);
        //    //this.SuppressWarning(typeof(IRepository<>), DiagnosticType.DisposableTransientComponent);
        //    //this.SuppressWarning(typeof(IRepositoryAsync<>), DiagnosticType.DisposableTransientComponent);
        //}

        //private void SuppressWarning(Type type, DiagnosticType supressType)
        //{
        //    var instanceProducer = Container.GetRegistration(type);

        //    if (instanceProducer != null)
        //    {
        //        var registration = instanceProducer.Registration;
        //        registration.SuppressDiagnosticWarning(supressType, supressType.ToString());
        //    }
        //}

        /// <summary>
        /// The register all.
        /// </summary>
        private void RegisterAll()
        {
            var autoInstallerClases = Assembly.GetCallingAssembly().GetExportedTypes().Where(x => typeof(IInstaller).IsAssignableFrom(x) && x.IsClass);

            foreach (var autoInstallerClass in autoInstallerClases)
            {
                var installer = Activator.CreateInstance(autoInstallerClass) as IInstaller;

                installer.Register(this.Container, this._httpConfiguration);
            }
        }
    }

    public class WebApiLifestyle : ILifestyleSelectionBehavior
    {
        public Lifestyle SelectLifestyle(Type serviceType, Type implementationType)
        {
            return WebApiRequestLifestyle.Scoped;
        }
    }
}
