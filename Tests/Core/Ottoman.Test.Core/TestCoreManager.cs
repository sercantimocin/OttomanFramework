namespace Ottoman.Test.Core
{
    using System.Web.Http.Dispatcher;

    /// <summary>
    /// The test core manager.
    /// </summary>
    public class TestCoreManager
    {
        /// <summary>
        /// The _instance.
        /// </summary>
        private static readonly TestCoreManager _instance = new TestCoreManager();

        /// <summary>
        /// The _assemblies resolver.
        /// </summary>
        private static TestAssembliesResolver _assembliesResolver;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static TestCoreManager Instance => _instance;

        /// <summary>
        /// Assemblies resolver.
        /// </summary>
        public IAssembliesResolver AssembliesResolver => _assembliesResolver;

        /// <summary>
        /// Prevents a default instance of the <see cref="TestCoreManager"/> class from being created.
        /// </summary>
        private TestCoreManager()
        {
            _assembliesResolver = new TestAssembliesResolver();
        }
    }
}
