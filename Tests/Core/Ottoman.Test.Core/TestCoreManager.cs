namespace Ottoman.Test.Core
{
    using System.Configuration;
    using System.Web.Http;
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
        /// Prevents a default instance of the <see cref="TestCoreManager"/> class from being created.
        /// </summary>
        private TestCoreManager()
        {
            _assembliesResolver = new TestAssembliesResolver();
        }

        /// <summary>
        /// The get assemblies resolver.
        /// </summary>
        /// <param name="projectNames">
        /// The project names.
        /// </param>
        /// <returns>
        /// The <see cref="IAssembliesResolver"/>.
        /// </returns>
        public IAssembliesResolver GetAssembliesResolver(string projectNames)
        {
            _assembliesResolver.ProjectNames = projectNames;
            return _assembliesResolver;
        }

        /// <summary>
        /// The change app config value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public void ChangeAppConfigValue(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// The configure http configuration.
        /// </summary>
        /// <param name="projectNames">
        /// The project names.
        /// </param>
        /// <returns>
        /// The <see cref="HttpConfiguration"/>.
        /// </returns>
        public HttpConfiguration ConfigureHttpConfiguration(string projectNames)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IAssembliesResolver), this.GetAssembliesResolver(projectNames));
            return GlobalConfiguration.Configuration;
        }
    }
}
