﻿using System.Web;
using System.Web.Mvc;

namespace TechmerVision
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A filter configuration. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class FilterConfig
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Registers the global filters described by filters. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="filters">  The filters. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}