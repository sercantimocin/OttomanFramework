namespace Ottoman.Injector.Policies.ClassInterface
{
    using System.Configuration;
    using System.Linq;
    using System.Reflection;

    using Ottoman.Core;

    using Repository.Pattern.DataContext;
    using Repository.Pattern.Ef6;
    using Repository.Pattern.Repositories;
    using Repository.Pattern.UnitOfWork;

    using Service.Pattern;
    using SimpleInjector;

    public class Installer : IInstaller
    {
        private const string EntitiesProjectName = "EntitiesProjectName";

        public void Register(Container container)
        {
            string entitiesProjectName = ConfigurationManager.AppSettings[EntitiesProjectName];

            if (!string.IsNullOrEmpty(entitiesProjectName))
            {
                Assembly entitiesAssembly = null;

                try
                {
                    entitiesAssembly = Assembly.Load(entitiesProjectName);
                }
                catch
                {
                    // ignored
                }

                if (entitiesAssembly != null)
                {
                    var contextType = entitiesAssembly.ExportedTypes.FirstOrDefault(x => typeof(IDataContextAsync).IsAssignableFrom(x) && x.IsClass);

                    if (contextType != null)
                    {
                        container.Register(typeof(IDataContextAsync), contextType);
                        container.Register(typeof(IUnitOfWorkAsync), typeof(UnitOfWork));
                        container.Register(typeof(IRepositoryAsync<>), typeof(Repository<>));
                        container.Register(typeof(IService<>), typeof(Service<>));
                    }
                }
            }
        }
    }
}
