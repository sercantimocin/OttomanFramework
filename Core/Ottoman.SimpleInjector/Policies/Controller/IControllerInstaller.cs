namespace Ottoman.Injector.Policies.Controller
{
    using System.Reflection;
    using System.Web.Http;

    using SimpleInjector;

    /// <summary>
    /// The ControllerInstaller interface.
    /// </summary>
    interface IControllerInstaller
    {
        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="httpConfiguration">
        /// The http configuration.
        /// </param>
        /// <param name="assemblies">
        /// The assemblies.
        /// </param>
        void Register(Container container, HttpConfiguration httpConfiguration, Assembly[] assemblies);
    }
}
