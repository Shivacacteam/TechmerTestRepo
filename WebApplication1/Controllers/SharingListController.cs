using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TechmerVision.Providers;
using TechmerVision.DAL;
using TechmerVision.Models;

namespace TechmerVision.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling product templates. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Authorize]
    public class SharingListController : ApiController
    {
        private WorkspaceContext db = new WorkspaceContext();
        private ApplicationUserManager _userManager;
        private string assetType = "ProductTemplates";
        private string UserName = "Techmer";
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the manager for user. </summary>
        ///
        /// <value> The user manager. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public SharingListController()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the Sharing templates in this collection. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the sharing templates in this
        ///     collection.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/SharingTemplates
        public IEnumerable<Sharing> GetTemplates()
        {
            List<Sharing> Sharings = db.Sharing.ToList();
            return Sharings.ToArray();
        }


       
        [ResponseType(typeof(Sharing))]
        public IHttpActionResult DeleteSharingTemplate(long id)
        {
            Sharing sharingTemplate = db.Sharing.Find(id);
            if (sharingTemplate == null)
            {
                return NotFound();
            }

            db.Sharing.Remove(sharingTemplate);
            db.SaveChanges();

            return Ok(sharingTemplate);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SharingTemplateExists(long id)
        {
            return db.Sharing.Count(e => e.Id == id) > 0;
        }

    }
}