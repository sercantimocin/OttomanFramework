﻿namespace Ottoman.Injector.Policies.Repository
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
    public class RepositoryInstaller : IRepositoryInstaller
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
                catch (Exception ) { }


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
