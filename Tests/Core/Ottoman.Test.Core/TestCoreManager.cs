namespace Ottoman.Test.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The test core manager.
    /// </summary>
    public class TestCoreManager
    {
        /// <summary>
        /// The _instance.
        /// </summary>
        private static TestCoreManager _instance;

        /// <summary>
        /// The _assemblies resolver.
        /// </summary>
        private static TestAssembliesResolver _assembliesResolver;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static TestCoreManager Instance => _instance;

        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        public ICollection<Assembly> Assemblies => _assembliesResolver.GetAssemblies();

        /// <summary>
        /// Prevents a default instance of the <see cref="TestCoreManager"/> class from being created.
        /// </summary>
        private TestCoreManager()
        {
            _instance = new TestCoreManager();
            _assembliesResolver = new TestAssembliesResolver();
        }
    }
}
