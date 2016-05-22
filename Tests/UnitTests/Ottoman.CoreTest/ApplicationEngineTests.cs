namespace Ottoman.CoreTest
{
    using System.Web.Http;
    using System.Web.Http.Dispatcher;

    using NUnit.Framework;

    using Ottomon.Core;

    [TestFixture()]
    public class ApplicationEngineTests
    {
        [Test()]
        public void InitializeTest()
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IAssembliesResolver), new TestAssembliesResolver());
            var httpConfiguration = GlobalConfiguration.Configuration;

            ApplicationEngine.Initialize(httpConfiguration);

            var container = ApplicationEngine.Container;

            Assert.IsNotNull(container);
        }
    }
}