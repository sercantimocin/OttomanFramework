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
            this._container = new Container();
        }

        /// <summary>
        /// The Initialize test.
        /// </summary>
        /// <param name="projectName">
        /// The project Name.
        /// </param>
        [TestCase("Sample.WebApi")]
        [TestCase(null)]
        public void WebApiInitializeTest(string projectName)
        {
            _httpConfiguration = TestCoreManager.Instance.ConfigureHttpConfiguration(projectName);

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
        [TestCase("Sample.Mvc.Client")]
        [TestCase(null)]
        public void MvcInitializeTest(string projectName)
        {
            _httpConfiguration = TestCoreManager.Instance.ConfigureHttpConfiguration(projectName);

            ApplicationEngine.MvcInitialize(_httpConfiguration, projectName);

            var container = ApplicationEngine.Container;

            Assert.IsNotNull(container);
        }
    }
}