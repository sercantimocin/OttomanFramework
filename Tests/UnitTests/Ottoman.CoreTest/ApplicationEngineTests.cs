namespace Ottoman.CoreTest
{
    using System.Web.Http;
    using System.Web.Http.Dispatcher;

    using NUnit.Framework;

    using Core;

    /// <summary>
    /// The application engine tests.
    /// </summary>
    [TestFixture(Category = "Core")]
    public class ApplicationEngineTests
    {
        /// <summary>
        /// The ınitialize test.
        /// </summary>
        [TestCase("Template.WebApi")]
        [TestCase(null)]
        public void InitializeTest(string projectName)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IAssembliesResolver), new TestAssembliesResolver());
            var httpConfiguration = GlobalConfiguration.Configuration;

            ApplicationEngine.WebApiInitialize(httpConfiguration, projectName);

            var container = ApplicationEngine.Container;

            Assert.IsNotNull(container);
        }
    }
}