﻿namespace Ottoman.CoreTest.Injection
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;

    using NUnit.Framework;

    using Injector.Policies.Controller;
    using Injector.Policies.Repository;

    using Ottoman.Test.Core;

    using Repository.Pattern.DataContext;
    using Repository.Pattern.UnitOfWork;

    using SimpleInjector;
    using SimpleInjector.Diagnostics;

    [TestFixture(Category = "Injector")]
    public class PoliciesTests
    {
        private HttpConfiguration _httpConfiguration;

        private Container _container;

        [SetUp]
        public void Init()
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IAssembliesResolver), TestCoreManager.Instance.AssembliesResolver);
            this._httpConfiguration = GlobalConfiguration.Configuration;

            this._container = new Container();
        }

        [Test()]
        public void WebApiControllerInstallerTest()
        {
            WebApiControllerInstaller installer = new WebApiControllerInstaller();

            installer.Register(this._container, this._httpConfiguration);

            this._container.Verify();

            //var controllerRegistration = this._container.GetRegistration(typeof(ApiController));

            //Assert.IsNotNull(controllerRegistration);
        }

        [Test()]
        public void MvcControllerInstallerTest()
        {
            MvcControllerInstaller installer = new MvcControllerInstaller();

            installer.Register(this._container, _httpConfiguration);

            this._container.Verify();

            //var controllerRegistration = this._container.GetRegistration(typeof(ApiController));

            //Assert.IsNotNull(controllerRegistration);
        }

        [Test()]
        public void RepositoryInstallerTest()
        {
            IInstaller installer = new Installer();

            installer.Register(this._container);

            this.SuppressRepositoryInstallerWarnings(this._container);

            this._container.Verify();

            var dataContextRegistration = this._container.GetRegistration(typeof(IDataContextAsync));

            Assert.IsNotNull(dataContextRegistration);
        }

        /// <summary>
        /// The suppress repository ınstaller warnings.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        private void SuppressRepositoryInstallerWarnings(Container container)
        {
            this.SuppressWarning(typeof(IDataContext), DiagnosticType.DisposableTransientComponent, container);
            this.SuppressWarning(typeof(IDataContextAsync), DiagnosticType.DisposableTransientComponent, container);
            this.SuppressWarning(typeof(IUnitOfWork), DiagnosticType.DisposableTransientComponent, container);
            this.SuppressWarning(typeof(IUnitOfWorkAsync), DiagnosticType.DisposableTransientComponent, container);
            //this.SuppressWarning(typeof(IRepository<>), DiagnosticType.DisposableTransientComponent, container);
            //this.SuppressWarning(typeof(IRepositoryAsync<>), DiagnosticType.DisposableTransientComponent, container);
        }

        /// <summary>
        /// The suppress warning.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="supressType">
        /// The supress type.
        /// </param>
        /// <param name="container">
        /// The container.
        /// </param>
        private void SuppressWarning(Type type, DiagnosticType supressType, Container container)
        {
            var instanceProducer = container.GetRegistration(type);

            if (instanceProducer != null)
            {
                var registration = instanceProducer.Registration;
                registration.SuppressDiagnosticWarning(supressType, supressType.ToString());
            }
        }
    }
}