namespace Ottoman.Injector.Policies.Repository
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;

    using global::Repository.Pattern.DataContext;
    using global::Repository.Pattern.Ef6;
    using global::Repository.Pattern.Repositories;
    using global::Repository.Pattern.UnitOfWork;

    using Service.Pattern;

    using SimpleInjector;

    /// <summary>
    /// The repository ınstaller.
    /// </summary>
    public class RepositoryInstaller : IInstaller
    {
        /// <summary>
        /// The entities project name.
        /// </summary>
        private const string EntitiesProjectName = "EntitiesProjectName";

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="httpConfiguration">
        /// The http configuration.
        /// </param>
        public void Register(Container container, HttpConfiguration httpConfiguration)
        {
            string entitiesProjectName = ConfigurationManager.AppSettings[EntitiesProjectName];

            if (!string.IsNullOrEmpty(entitiesProjectName))
            {
                Assembly entitiesAssembly = null;

                try
                {
                    entitiesAssembly = Assembly.Load(entitiesProjectName);
                }
                catch (Exception ex) { }


                if (entitiesAssembly != null)
                {
                    var contextType = entitiesAssembly.ExportedTypes.FirstOrDefault(x => typeof(IDataContextAsync).IsAssignableFrom(x) && x.IsClass);

                    if (contextType != null)
                    {
                        //container.Register(typeof(IDataContext), contextType, WebApiRequestLifestyle.Scoped);
                        container.Register(typeof(IDataContextAsync), contextType);

                        //container.Register(typeof(IUnitOfWork), typeof(UnitOfWork), WebApiRequestLifestyle.Scoped);
                        container.Register(typeof(IUnitOfWorkAsync), typeof(UnitOfWork));

                        //container.Register(typeof(IRepository<>), typeof(Repository<>), WebApiRequestLifestyle.Scoped);
                        container.Register(typeof(IRepositoryAsync<>), typeof(Repository<>));

                        container.Register(typeof(IService<>), typeof(Service<>));

                        //var classes = entitiesAssembly.ExportedTypes.Where(x => typeof(BaseEntity).IsAssignableFrom(x)).ToList();

                        //container.RegisterConditional(typeof(IRepository<>), typeof(Repository<>), Lifestyle.Transient,
                        //    c => classes.Contains(c.Consumer.ServiceType));

                        //container.RegisterConditional(typeof(IRepositoryAsync<>), typeof(Repository<>), Lifestyle.Transient,
                        //    c => classes.Contains(c.Consumer.ImplementationType.ReflectedType));

                        //container.RegisterConditional(typeof(IService<>), typeof(Service<>), Lifestyle.Scoped,
                        //    c => classes.Contains(c.Consumer.ServiceType));

                    }
                }
            }
        }
    }
}
