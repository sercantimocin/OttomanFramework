namespace Ottoman.Test.Core
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Reflection;
    using System.Web.Http.Dispatcher;

    /// <summary>
    /// The test assemblies resolver.
    /// </summary>
    internal class TestAssembliesResolver : IAssembliesResolver
    {
        /// <summary>
        /// The entities project name.
        /// </summary>
        private const string ProjectName = "ProjectName";

        /// <summary>
        /// The get assemblies.
        /// </summary>
        /// <returns>
        /// The <see cref="ICollection"/>.
        /// </returns>
        public ICollection<Assembly> GetAssemblies()
        {
            List<Assembly> assemblyList = new List<Assembly>();

            string apiProjectNames = ConfigurationManager.AppSettings[ProjectName];

            if (!string.IsNullOrEmpty(apiProjectNames))
            {
                string[] projectNames = apiProjectNames.Split('|');

                foreach (var projectName in projectNames)
                {
                    try
                    {
                        assemblyList.Add(Assembly.Load(projectName));
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }

            return assemblyList;
        }
    }
}
