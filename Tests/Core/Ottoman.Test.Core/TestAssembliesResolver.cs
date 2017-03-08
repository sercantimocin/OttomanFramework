namespace Ottoman.Test.Core
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Http.Dispatcher;

    /// <summary>
    /// The test assemblies resolver.
    /// </summary>
    internal class TestAssembliesResolver : IAssembliesResolver
    {
        /// <summary>
        /// Gets or sets the project names.
        /// </summary>
        public string ProjectNames { get; set; }

        /// <summary>
        /// The get assemblies.
        /// </summary>
        /// <returns>
        /// The <see cref="ICollection"/>.
        /// </returns>
        public ICollection<Assembly> GetAssemblies()
        {
            List<Assembly> assemblyList = new List<Assembly>();

            if (!string.IsNullOrEmpty(this.ProjectNames))
            {
                string[] projectNamesArray = this.ProjectNames.Split('|');

                foreach (var projectName in projectNamesArray)
                {
                    try
                    {
                        assemblyList.Add(Assembly.Load(projectName));
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            return assemblyList;
        }
    }
}
