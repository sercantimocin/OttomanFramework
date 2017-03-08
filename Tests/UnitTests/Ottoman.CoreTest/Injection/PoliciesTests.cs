//----------------------------------------------------------------------------------------------------------------------
// <copyright file="policiestests.cs" owner="Sercan Timoçin" namespace="Ottoman.CoreTest">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>devpc</author>
// <date>2016-5-22 08:23</date>
//---------------------------------------------------------------------------------------------------------------------- 
namespace Ottoman.CoreTest.Injection
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Mvc;

    using NUnit.Framework;

    using Ottoman.Injector.Policies.ClassInterface;
    using Ottoman.Injector.Policies.Controller;
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
            this._container = new Container();
        }

        [TestCase("Sample.WebApi")]
        public void WebApiControllerInstallerTest(string projectName)
        {
            _httpConfiguration = TestCoreManager.Instance.ConfigureHttpConfiguration(projectName);

            WebApiControllerInstaller installer = new WebApiControllerInstaller();

            installer.Register(this._container, this._httpConfiguration);

            this._container.Verify();

            var controllerRegistration = this._container.GetRegistration(typeof(ApiController));

            Assert.IsNotNull(controllerRegistration);
        }

        [TestCase("Sample.Mvc.Client")]
        public void MvcControllerInstallerTest(string projectName)
        {
            _container = new Container();
            _httpConfiguration = TestCoreManager.Instance.ConfigureHttpConfiguration(projectName);

            MvcControllerInstaller installer = new MvcControllerInstaller();

            installer.Register(this._container, _httpConfiguration);

            this._container.Verify();

            var registrations = this._container.GetCurrentRegistrations();

            Assert.IsNotNull(registrations);
            //Assert.That(registrations.Any(x => typeof(Controller).IsAssignableFrom(x.ServiceType)));
        }

        [Test()]
        public void InstallerTest()
        {
            IInstaller installer = new Installer();

            installer.Register(this._container);

            this.SuppressRepositoryInstallerWarnings(this._container);

            this._container.Verify();

            var registrations = this._container.GetCurrentRegistrations();

            Assert.IsNotNull(registrations);
            Assert.That(registrations.Any(x => typeof(IDataContextAsync).IsAssignableFrom(x.ServiceType)));
        }

        [TearDown]
        public void TearDown()
        {
            this._container.Dispose();
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