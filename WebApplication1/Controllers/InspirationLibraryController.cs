using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechmerVision.DAL;
using TechmerVision.Models;
using TechmerVision.Providers;

namespace TechmerVision.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling inspiration libraries. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [RoutePrefix("Admin/InspirationLibrary")]
    [Route("{action=index}")]
    public class InspirationLibraryController : Controller
    {

        WorkspaceContext db = new WorkspaceContext();
        private ApplicationUserManager _userManager;
        private string assetType = "SampleInspirations";
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
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the index. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the Index View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: Admin/InspirationLibrary
        public ActionResult Index()
        {
            List<SampleInspiration> ret = new List<SampleInspiration>();
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);

            List<SampleInspiration> sampleInspirations = db.SampleInspirations.ToList();
            ret = imageStorage.getImages(UserName, assetType, sampleInspirations);

            return View(ret);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Details the given identifier. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   A response stream to send to the Details View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: Admin/InspirationLibrary/Details/5
        public ActionResult Details(long? id)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SampleInspiration sampleInspiration = db.SampleInspirations.Find(id);
            if (sampleInspiration == null)
            {
                return HttpNotFound();
            }
            sampleInspiration.Image = imageStorage.getImage(UserName, assetType, sampleInspiration);
            return View(sampleInspiration);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) creates a new ActionResult. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the Create View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: SampleInspirations1/Create
        public ActionResult Create()
        {
            return View();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) creates a new ActionResult.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="sampleInspiration">    The sample inspiration. </param>
        /// <param name="file">                 The file. </param>
        ///
        /// <returns>   A response stream to send to the Create View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: Admin/InspirationLibrary/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Image,Active")] SampleInspiration sampleInspiration, HttpPostedFileBase file)
        {
            CloudStorageProvider imageStorage  = new CloudStorageProvider(UserManager);

            if (ModelState.IsValid)
            {

                if (file != null && file.ContentLength > 0)
                {
                    sampleInspiration.Filename = imageStorage.saveImage(UserName, assetType, file);
                    db.SampleInspirations.Add(sampleInspiration);
                    db.SaveChanges();
                    
                    return RedirectToAction("Index");
                }
                                
            }

            return View(sampleInspiration);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) edits the given sample inspiration.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   A response stream to send to the Edit View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: Admin/InspirationLibrary/Edit/5
        public ActionResult Edit(long? id)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SampleInspiration sampleInspiration = db.SampleInspirations.Find(id);
            if (sampleInspiration == null)
            {
                return HttpNotFound();
            }
            sampleInspiration.Image = imageStorage.getImage(UserName, assetType, sampleInspiration);
            return View(sampleInspiration);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) edits the given sample inspiration.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="sampleInspiration">    The sample inspiration. </param>
        ///
        /// <returns>   A response stream to send to the Edit View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: Admin/InspirationLibrary/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Image,Active")] SampleInspiration sampleInspiration)
        {
            if (ModelState.IsValid)
            {
                SampleInspiration ret = db.SampleInspirations.Where(s => s.Id == sampleInspiration.Id).First();
                ret.Title = sampleInspiration.Title;
                ret.Active = sampleInspiration.Active;
                db.Entry(ret).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sampleInspiration);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the given ID. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   A response stream to send to the Delete View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: SampleInspirations1/Delete/5
        public ActionResult Delete(long? id)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SampleInspiration sampleInspiration = db.SampleInspirations.Find(id);
            if (sampleInspiration == null)
            {
                return HttpNotFound();
            }
            sampleInspiration.Image = imageStorage.getImage(UserName, assetType, sampleInspiration);
            return View(sampleInspiration);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) (Defines the Delete Action) deletes the
        ///     confirmed described by ID.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   A response stream to send to the DeleteConfirmed View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: SampleInspirations1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            SampleInspiration sampleInspiration = db.SampleInspirations.Find(id);
            imageStorage.deleteImage(UserName, assetType, sampleInspiration);
            db.SampleInspirations.Remove(sampleInspiration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Releases unmanaged resources and optionally releases managed resources. </summary>
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
    }
}
