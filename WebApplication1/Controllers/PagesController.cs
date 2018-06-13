using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechmerVision.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling pages. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Authorize]
    public class PagesController : Controller
    {

        //Need to udpate file in order for merging to take place.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the index. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the Index View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: Pages
        public ActionResult Index()
        {
            return View();


        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the inspiration. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the Inspiration View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //Get: Pages/Inspiration
        public ActionResult Inspiration()
        {
            return PartialView();
        }
    }
}