using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TechmerVision.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling homes. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [AllowAnonymous]
    public class HomeController : Controller
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the index. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the Index View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Thank you. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the ThankYou View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [AllowAnonymous]
        public ActionResult ThankYou()
        {
            return View();
        }
    }
}
