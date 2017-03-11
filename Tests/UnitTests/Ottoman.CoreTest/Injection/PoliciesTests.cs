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

    using NUnit.Framework;

    using Ottoman.Injector.Policies.ClassInterface;
    using Ottoman.Injector.Policies.Controller;
    using Ottoman.Test.Core;

    using Repository.Pattern.DataContext;
    using Repository.Pattern.Ef6;
    using Repository.Pattern.Repositories;
    using Repository.Pattern.UnitOfWork;

    using Service.Pattern;

    using SimpleInjector;
    using SimpleInjector.Diagnostics;
    using SimpleInjector.Integration.Web;

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

        [TestCase("Demo.WebApi|Demo.Entities")]
        public void WebApiControllerInstallerTest(string projectName)
        {
            _httpConfiguration = TestCoreManager.Instance.ConfigureHttpConfiguration(projectName);

            WebApiControllerInstaller installer = new WebApiControllerInstaller();
            var contextType = _httpConfiguration.Services
                                                .GetAssembliesResolver()
                                                .GetAssemblies()
                                                .SelectMany(x => x.ExportedTypes)
                                                .FirstOrDefault(x => typeof(IDataContextAsync).IsAssignableFrom(x)
                                                                     && x.IsClass);
            _container.Options.DefaultLifestyle = new WebRequestLifestyle();
            _container.Register(typeof(IDataContextAsync), contextType);
            _container.Register(typeof(IUnitOfWorkAsync), typeof(UnitOfWork));
            _container.Register(typeof(IRepositoryAsync<>), typeof(Repository<>));
            _container.Register(typeof(IService<>), typeof(Service<>));


            installer.Register(this._container, this._httpConfiguration);

            this._container.Verify();

            var registrations = this._container.GetCurrentRegistrations();

            Assert.IsNotNull(registrations);
            Assert.That(registrations.Any(x => typeof(ApiController).IsAssignableFrom(x.ServiceType)));
        }

        [TestCase("Demo.Mvc.Client")]
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