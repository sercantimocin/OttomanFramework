//----------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationEngine.cs" owner="Sercan Timoçin" namespace="Ottomon.Core">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 20:30</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottomon.Core
{
    using Ottoman.SimpleInjector;

    /// <summary>
    /// The application engine.
    /// </summary>
    public static class ApplicationEngine
    {
        /// <summary>
        /// Initializes static members of the <see cref="ApplicationEngine"/> class.
        /// </summary>
        static ApplicationEngine()
        {
            new SimpleInjectorManager();
        }
    }
}
