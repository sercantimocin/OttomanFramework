//----------------------------------------------------------------------------------------------------------------------
// <copyright file="Installer.cs" owner="Sercan Timoçin" namespace="Ottoman.SimpleInjector">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 22:11</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottoman.SimpleInjector.Auto
{
    using System.Reflection;

    using global::SimpleInjector;

    /// <summary>
    /// The nstaller interface.
    /// </summary>
    public interface Installer
    {
        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <param name="container">
        /// The container.
        /// </param>
        void Register(Assembly[] assembly, Container container);
    }
}
