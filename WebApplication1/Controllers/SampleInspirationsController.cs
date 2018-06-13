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
    /// <summary>   A controller for handling sample inspirations. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class SampleInspirationController : ApiController
    {
        private WorkspaceContext db = new WorkspaceContext();
        private ApplicationUserManager _userManager;
        private string assetType = "SampleInspirations";
        private string UserName = "Techmer";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public SampleInspirationController()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="userManager">  The user manager. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public SampleInspirationController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

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


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the sample inspirations in this collection. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the sample inspirations in this
        ///     collection.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/SampleInspirations
        public IEnumerable<SampleInspiration> GetSampleInspirations()
        {
            List<SampleInspiration> ret = new List<SampleInspiration>();
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);

            var user = UserManager.FindById(User.Identity.GetUserId());

            List<SampleInspiration> sampleInspirations = db.SampleInspirations.Where(r => r.Active == true).ToList<SampleInspiration>();
            ret = imageStorage.getImages(UserName, assetType, sampleInspirations);

            return ret.ToArray();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets sample inspiration. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   The sample inspiration. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/SampleInspirations/5
        [ResponseType(typeof(SampleInspiration))]
        public IHttpActionResult GetSampleInspiration(long id)
        {
            SampleInspiration sampleInspiration = db.SampleInspirations.Find(id);
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            sampleInspiration.Image = imageStorage.getImage(UserName, assetType, sampleInspiration);
            if (sampleInspiration == null)
            {
                return NotFound();
            }

            return Ok(sampleInspiration);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Puts sample inspiration. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <exception cref="DbUpdateConcurrencyException"> Thrown when a Database Update Concurrency
        ///                                                 error condition occurs. </exception>
        ///
        /// <param name="id">                   The identifier. </param>
        /// <param name="sampleInspiration">    The sample inspiration. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // PUT: api/SampleInspirations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSampleInspiration(long id, SampleInspiration sampleInspiration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sampleInspiration.Id)
            {
                return BadRequest();
            }

            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            sampleInspiration.Filename = imageStorage.saveImage(UserName, assetType, sampleInspiration);


            db.Entry(sampleInspiration).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SampleInspirationExists(id))
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


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Posts a sample inspiration. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="sampleInspiration">    The sample inspiration. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/SampleInspirations
        [ResponseType(typeof(SampleInspiration))]
        public IHttpActionResult PostSampleInspiration(SampleInspiration sampleInspiration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            sampleInspiration.Filename = imageStorage.saveImage(UserName, assetType, sampleInspiration);
            db.SampleInspirations.Add(sampleInspiration);
            db.SaveChanges();

            sampleInspiration.Image = imageStorage.getImage(UserName, assetType, sampleInspiration);

            return CreatedAtRoute("DefaultApi", new { id = sampleInspiration.Id }, sampleInspiration);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the sample inspiration described by ID. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // DELETE: api/SampleInspirations/5
        [ResponseType(typeof(SampleInspiration))]
        public IHttpActionResult DeleteSampleInspiration(long id)
        {
            SampleInspiration sampleInspiration = db.SampleInspirations.Find(id);
            if (sampleInspiration == null)
            {
                return NotFound();
            }

            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            imageStorage.deleteImage(UserName, assetType, sampleInspiration);

            db.SampleInspirations.Remove(sampleInspiration);
            db.SaveChanges();

            return Ok(sampleInspiration);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Releases the unmanaged resources that are used by the object and, optionally, releases
        ///     the managed resources.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="disposing">    true to release both managed and unmanaged resources; false to
        ///                             release only unmanaged resources. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SampleInspirationExists(long id)
        {
            return db.SampleInspirations.Count(e => e.Id == id) > 0;
        }
    }
}