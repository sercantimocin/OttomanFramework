//----------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterConfig.cs" owner="Sercan Timoçin" namespace="Template.WebApi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>devpc</author>
// <date>2016-5-17 22:34</date>
//---------------------------------------------------------------------------------------------------------------------- 

namespace Demo.WebApi
{
    using System.Web.Mvc;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
