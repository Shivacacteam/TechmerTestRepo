using System;
using System.Web.Http;
using System.Web.Mvc;
using TechmerVision.Areas.HelpPage.ModelDescriptions;
using TechmerVision.Areas.HelpPage.Models;

namespace TechmerVision.Areas.HelpPage.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   The controller that will handle requests for the help page. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="config">   The configuration. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the configuration. </summary>
        ///
        /// <value> The configuration. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public HttpConfiguration Configuration { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the index. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the Index View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Apis. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="apiId">    Identifier for the API. </param>
        ///
        /// <returns>   A response stream to send to the API View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Resource model. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="modelName">    Name of the model. </param>
        ///
        /// <returns>   A response stream to send to the ResourceModel View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}