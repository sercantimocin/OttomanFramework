//----------------------------------------------------------------------------------------------------------------------
// <copyright file="OttomanContainer.cs" owner="Sercan Timoçin" namespace="Ottoman.SimpleInjector">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-12 21:06</date>
//---------------------------------------------------------------------------------------------------------------------- 
namespace Ottoman.SimpleInjector
{
    /// <summary>
    /// The ottoman container.
    /// </summary>
    /// <typeparam name="T">
    /// Container class 
    /// </typeparam>
    public class OttomanContainer<T> : IOttomanContainer<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OttomanContainer{T}"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public OttomanContainer(T container)
        {
            this.Get = container;
        }

        /// <summary>
        /// Gets the get.
        /// </summary>
        public T Get { get; }
    }
}
