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
                IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains(projectName))
                                                                .SelectMany(a => a.ExportedTypes)
                                                                .Where(a => a.IsClass);

                LoadStandardMappings(types);
                LoadCustomMappings(types);

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
#pragma warning disable 618
                map.CreateMappings(Mapper.Configuration);
#pragma warning restore 618
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
#pragma warning disable CS0618 // Type or member is obsolete
                Mapper.CreateMap(map.Source, map.Destination).ReverseMap();
#pragma warning restore CS0618 // Type or member is obsolete
            }
        }
    }
}
