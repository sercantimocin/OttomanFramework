using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ottoman.Injector.Policies.Controller
{
    using System.Reflection;
    using System.Web.Http;

    using SimpleInjector;

    public class MvcControllerInstaller : IControllerInstaller
    {
        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="container">
        ///     The container.
        /// </param>
        /// <param name="assemblies">Mvc controller assemblies</param>
        public void Register(Container container, Assembly[] assemblies)
        {
            container.RegisterMvcControllers(assemblies);

            container.RegisterMvcIntegratedFilterProvider();
        }
    }
}
