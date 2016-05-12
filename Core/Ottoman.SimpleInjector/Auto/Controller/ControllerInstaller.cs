//----------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerInstaller.cs" owner="Sercan Timoçin" namespace="Ottoman.SimpleInjector">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 22:10</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottoman.SimpleInjector.Auto.Controller
{
    using System;
    using System.Reflection;

    using global::SimpleInjector;

    using Ottoman.SimpleInjector.Auto;

    /// <summary>
    /// The controller ınstaller.
    /// </summary>
    public class ControllerInstaller : Installer
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
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void Register(Assembly[] assembly, Container container)
        {
            throw new System.NotImplementedException();
        }
    }
}
