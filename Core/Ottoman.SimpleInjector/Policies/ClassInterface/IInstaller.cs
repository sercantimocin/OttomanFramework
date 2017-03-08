//----------------------------------------------------------------------------------------------------------------------
// <copyright file="IInstaller.cs" owner="Sercan Timoçin" namespace="Ottoman.SimpleInjector">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 22:11</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottoman.Injector.Policies.ClassInterface
{
    using SimpleInjector;

    /// <summary>
    /// The class interface matcher installer.
    /// </summary>
    public interface IInstaller
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
