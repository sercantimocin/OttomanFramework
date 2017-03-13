namespace Ottoman.Injector.Policies.Controller
{
    using System.Linq;
    using System.Web.Http;

    using SimpleInjector;

    /// <summary>
    /// The MVC controller installer.
    /// </summary>
    public class MvcControllerInstaller : IControllerInstaller
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
        public void Register(Container container, HttpConfiguration httpConfiguration)
        {
            container.RegisterMvcControllers(httpConfiguration.Services.GetAssembliesResolver().GetAssemblies().ToArray());
            container.RegisterMvcIntegratedFilterProvider();
        }
    }
}
