//----------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleInjectorManagerTests.cs" company="Sercan Timoçin" namespace="Ottoman.CoreTest">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 23:10</date>
//---------------------------------------------------------------------------------------------------------------------- 
namespace Ottoman.CoreTest
{
    using System;

    using NUnit.Framework;

    using Ottoman.SimpleInjector;

    /// <summary>
    /// The simple ınjector manager tests.
    /// </summary>
    [TestFixture]
    public class SimpleInjectorManagerTests
    {
        /// <summary>
        /// The ıs create simple ınjector manager.
        /// </summary>
        /// <exception cref="Exception">
        /// any Exception
        /// </exception>
        [Test]
        public void IsCreateSimpleInjectorManager()
        {
            new SimpleInjectorManager();

            Assert.DoesNotThrow(delegate { throw new Exception(); });
        }
    }
}