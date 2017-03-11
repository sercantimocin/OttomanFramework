//----------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoMapperManagerTests.cs" owner="Sercan Timoçin" namespace="Ottoman.CoreTest">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-24 10:02</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottoman.CoreTest.Mapper
{
    using NUnit.Framework;

    using Ottoman.Mapper;

    /// <summary>
    /// The auto mapper manager tests.
    /// </summary>
    [TestFixture(Category = "Mapper")]
    public class AutoMapperManagerTests
    {
        /// <summary>
        /// The register classes bulk test.
        /// </summary>
        /// <param name="projectName">
        /// The project name.
        /// </param>
        [TestCase(null)]
        [TestCase("WrongProjectName")]
        [TestCase("Demo.WebApi.Models")]
        public void RegisterClassesBulkTest(string projectName)
        {
            AutoMapperManager.RegisterClassesBulk(projectName);
        }
    }
}