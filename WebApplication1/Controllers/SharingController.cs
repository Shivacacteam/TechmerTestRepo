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
    public class SharingController : ApiController
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


        public SharingController()
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
        public IEnumerable<ProductTemplate> GetTemplates()
        {

            List<ProductTemplate> ret = new List<ProductTemplate>();
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            var user = UserManager.FindById(User.Identity.GetUserId());
            var sharingTemplates = db.Sharing.Where(st => st.UserId == user.UserName).ToList();
            foreach (var item in sharingTemplates)
            {
                var products = db.ProductTemplates.SingleOrDefault(r => r.Id == item.AssetId && r.Active == true);
                ret.Add(products);
            }
            ret = imageStorage.getImages(UserName, assetType, ret, true);
            return ret.ToArray();
        }




        // Update: api/ProductTemplates/2
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSharingTemplate(long id, Sharing sharing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sharing.Id)
            {
                return BadRequest();
            }

            db.Entry(sharing).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SharingTemplateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // Add: api/ProductTemplates/3
        [ResponseType(typeof(Sharing))]
        public IHttpActionResult PostSharingTemplate(Sharing sharing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sharing.Add(sharing);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = sharing.Id }, sharing);
        }


        // GET Currently Share With
        //[HttpPost]
        //[ActionName("CurrentShareWith")]
        //public IEnumerable<Sharing> CurrentShareWith()
        //{
        //    List<Sharing> Sharings = db.Sharing.Where(pt => pt.AssetId == 23).ToList();
        //    return Sharings.ToArray();
 
        //}




        // DELETE: api/ProductTemplates/4
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