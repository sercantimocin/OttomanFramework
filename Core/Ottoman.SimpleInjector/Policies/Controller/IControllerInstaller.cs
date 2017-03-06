namespace Ottoman.Injector.Policies.Controller
{
    using System.Web.Http;

    using SimpleInjector;

    /// <summary>
    /// The ControllerInstaller interface.
    /// </summary>
    internal interface IControllerInstaller
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
        void Register(Container container, HttpConfiguration httpConfiguration);
    }
}
