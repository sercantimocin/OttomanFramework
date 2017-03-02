//----------------------------------------------------------------------------------------------------------------------
// <copyright file="Installer.cs" owner="Sercan Timoçin" namespace="Ottoman.SimpleInjector">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 22:11</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottoman.Injector.Policies.Repository
{
    using SimpleInjector;

    /// <summary>
    /// The Installer interface.
    /// </summary>
    public interface IRepositoryInstaller
    {
        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        void Register(Container container);
    }
}
