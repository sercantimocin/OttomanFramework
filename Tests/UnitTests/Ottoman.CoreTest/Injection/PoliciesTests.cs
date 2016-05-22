namespace Ottoman.CoreTest.Injection
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;

    using NUnit.Framework;

    using Injector.Policies;
    using Injector.Policies.Controller;
    using Injector.Policies.Repository;

    using Repository.Pattern.DataContext;
    using Repository.Pattern.Repositories;
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

            GlobalConfiguration.Configuration.Services.Replace(typeof(IAssembliesResolver), new TestAssembliesResolver());
            this._httpConfiguration = GlobalConfiguration.Configuration;

            this._container = new Container();
        }

        [Test()]
        public void ControllerInstallerTest()
        {
            IInstaller installer = new ControllerInstaller();

            installer.Register(this._container, this._httpConfiguration);

            this._container.Verify();

            //var controllerRegistration = this._container.GetRegistration(typeof(ApiController));

            //Assert.IsNotNull(controllerRegistration);
        }

        [Test()]
        public void RepositoryInstallerTest()
        {
            IInstaller installer = new RepositoryInstaller();

            installer.Register(this._container, this._httpConfiguration);

            //this.SuppressRepositoryInstallerWarnings(this._container);

            this._container.Verify();

            var dataContextRegistration = this._container.GetRegistration(typeof(IDataContextAsync));

            Assert.IsNotNull(dataContextRegistration);
        }

        //private void SuppressRepositoryInstallerWarnings(Container container)
        //{
        //    this.SuppressWarning(typeof(IDataContext), DiagnosticType.DisposableTransientComponent, container);
        //    this.SuppressWarning(typeof(IDataContextAsync), DiagnosticType.DisposableTransientComponent, container);
        //    this.SuppressWarning(typeof(IUnitOfWork), DiagnosticType.DisposableTransientComponent, container);
        //    this.SuppressWarning(typeof(IUnitOfWorkAsync), DiagnosticType.DisposableTransientComponent, container);
        //    //this.SuppressWarning(typeof(IRepository<>), DiagnosticType.DisposableTransientComponent, container);
        //    //this.SuppressWarning(typeof(IRepositoryAsync<>), DiagnosticType.DisposableTransientComponent, container);
        //}

        //private void SuppressWarning(Type type, DiagnosticType supressType,Container container)
        //{
        //    var instanceProducer = container.GetRegistration(type);

        //    if (instanceProducer != null)
        //    {
        //        var registration = instanceProducer.Registration;
        //        registration.SuppressDiagnosticWarning(supressType, supressType.ToString());
        //    }
        //}
    }
}