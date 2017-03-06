namespace Ottoman.CoreTest
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Mvc;

    using Core;

    using NUnit.Framework;

    using Ottoman.Test.Core;

    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;

    /// <summary>
    /// The application engine tests.
    /// </summary>
    [TestFixture(Category = "Core")]
    public class ApplicationEngineTests
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

        /// <summary>
        /// The Initialize test.
        /// </summary>
        /// <param name="projectName">
        /// The project Name.
        /// </param>
        [TestCase("Template.WebApi")]
        [TestCase(null)]
        public void WebApiInitializeTest(string projectName)
        {
            ApplicationEngine.WebApiInitialize(_httpConfiguration, projectName);

            var container = ApplicationEngine.Container;

            Assert.IsNotNull(container);
        }

        /// <summary>
        /// The Initialize test.
        /// </summary>
        /// <param name="projectName">
        /// The project Name.
        /// </param>
        ///     
        [TestCase("Template.Client")]
        [TestCase(null)]
        public void MvcInitializeTest(string projectName)
        {
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(_container));

            ApplicationEngine.MvcInitialize(_httpConfiguration, projectName);

            var container = ApplicationEngine.Container;

            Assert.IsNotNull(container);
        }
    }
}