//----------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveCustomMappings.cs" owner="Sercan Timoçin" namespace="Ottoman.Mapper">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>SercanT</author>
// <date>2016-5-24 09:49</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Ottoman.Mapper
{
    using AutoMapper;

    /// <summary>
    /// The HaveCustomMappings interface.
    /// </summary>
    internal interface IHaveCustomMappings
    {
        /// <summary>
        /// The create mappings.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        void CreateMappings(IConfiguration configuration);
    }
}
