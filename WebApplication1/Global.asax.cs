using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TechmerVision
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A MVC application. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class MvcApplication : HttpApplication
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Application start. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
