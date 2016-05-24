//----------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoMapperManager.cs" owner="Sercan Timoçin" namespace="Ottoman.Mapper">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-24 09:25</date>
//---------------------------------------------------------------------------------------------------------------------- 
namespace Ottoman.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;

    /// <summary>
    /// The auto mapper manager.
    /// </summary>
    public static class AutoMapperManager
    {
        /// <summary>
        /// The register classes bulk.
        /// </summary>
        /// <param name="projectName">
        /// The project name.
        /// </param>
        public static void RegisterClassesBulk(string projectName)
        {
            if (!string.IsNullOrEmpty(projectName))
            {
                IEnumerable<Assembly> assembly = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains(projectName));

                if (assembly != null)
                {
                    IEnumerable<Type> types = assembly.SelectMany(a => a.ExportedTypes).Where(a => a.IsClass);

                    Type[] enumerable = types as Type[] ?? types.ToArray();
                    LoadStandardMappings(enumerable);
                    LoadCustomMappings(enumerable);
                }

                Mapper.Instance.ConfigurationProvider.AssertConfigurationIsValid();
            }
        }


        /// <summary>
        /// Loads the custom mappings.
        /// </summary>
        /// <param name="types">The types.</param>
        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(Mapper.Configuration);
            }
        }

        /// <summary>
        /// Loads the standard mappings.
        /// </summary>
        /// <param name="types">The types.</param>
        private static void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType &&
                              i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                Mapper.CreateMap(map.Source, map.Destination);
                Mapper.CreateMap(map.Destination, map.Source);
            }
        }
    }
}
