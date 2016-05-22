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

    public class RepositoryInstaller : IInstaller
    {
        private const string EntitiesProjectName = "EntitiesProjectName";

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
                        container.Register(typeof(IDataContext), contextType, Lifestyle.Transient);
                        container.Register(typeof(IDataContextAsync), contextType, Lifestyle.Transient);

                        container.Register(typeof(IUnitOfWork), typeof(UnitOfWork), Lifestyle.Transient);
                        container.Register(typeof(IUnitOfWorkAsync), typeof(UnitOfWork), Lifestyle.Transient);

                        container.Register(typeof(IRepository<>), new[] { entitiesAssembly }, Lifestyle.Transient);
                        container.Register(typeof(IRepositoryAsync<>), new[] { entitiesAssembly }, Lifestyle.Transient);

                        container.Register(typeof(IService<>), new[] { entitiesAssembly }, Lifestyle.Scoped);
                    }
                }
            }
        }
    }
}
