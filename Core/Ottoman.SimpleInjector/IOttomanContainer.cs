//----------------------------------------------------------------------------------------------------------------------
// <copyright file="IOttomanContainer.cs" owner="Sercan Timoçin" namespace="Ottoman.SimpleInjector">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 21:03</date>
//---------------------------------------------------------------------------------------------------------------------- 
namespace Ottoman.SimpleInjector
{
    /// <summary>
    /// The OttomanContainer interface.
    /// </summary>
    /// <typeparam name="T">
    /// Generic Container Type
    /// </typeparam>
    public interface IOttomanContainer<T>
    {
        /// <summary>
        /// Gets the get.
        /// </summary>
        T Get { get; }
    }
}
