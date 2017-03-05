namespace Ottoman.CoreTest
{
    using System.Web.Http;
    using System.Web.Http.Dispatcher;

    using Core;

    using NUnit.Framework;

    using Ottoman.Test.Core;

    /// <summary>
    /// The application engine tests.
    /// </summary>
    [TestFixture(Category = "Core")]
    public class ApplicationEngineTests
    {
        /// <summary>
        /// The Initialize test.
        /// </summary>
        /// <param name="projectName">
        /// The project Name.
        /// </param>
        [TestCase("Template.WebApi")]
        [TestCase(null)]
        public void InitializeTest(string projectName)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IAssembliesResolver), TestCoreManager.Instance.Assemblies);
            var httpConfiguration = GlobalConfiguration.Configuration;

            ApplicationEngine.WebApiInitialize(httpConfiguration, projectName);

            var container = ApplicationEngine.Container;

            Assert.IsNotNull(container);
        }
    }
}