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
    /// <summary>   A controller for handling admin product templates. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [RoutePrefix("Admin/ProductTemplate")]
    [Route("{action=index}")]
    public class AdminProductTemplatesController : Controller
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

        // GET: Admin/ProductTemplates
        public ActionResult Index()
        {
            List<ProductTemplate> ret = new List<ProductTemplate>();
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            List<ProductTemplate> productTemplates = db.ProductTemplates.ToList();
            ret = imageStorage.getImages(UserName, assetType, productTemplates, false);
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

        // GET: Admin/ProductTemplates/Details/5
        public ActionResult Details(long? id)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTemplate productTemplate = db.ProductTemplates.Find(id);
            if (productTemplate == null)
            {
                return HttpNotFound();
            }
            productTemplate = imageStorage.getImage(UserName, assetType, productTemplate, true);
            return View(productTemplate);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) creates a new ActionResult. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the Create View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: Admin/ProductTemplates/Create
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
        /// <param name="productTemplate">  The product template. </param>
        /// <param name="file">             The file. </param>
        ///
        /// <returns>   A response stream to send to the Create View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: Admin/ProductTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Image,HasBackgroundImage,BackgroundImage,NumColors,Active")] ProductTemplate productTemplate, HttpPostedFileBase file)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);

            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    productTemplate.FileName = imageStorage.saveImage(UserName, assetType, file);
                    db.ProductTemplates.Add(productTemplate);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(productTemplate);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) edits. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   A response stream to send to the Edit View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: Admin/ProductTemplates/Edit/5
        public ActionResult Edit(long? id)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTemplate productTemplate = db.ProductTemplates.Find(id);
            if (productTemplate == null)
            {
                return HttpNotFound();
            }
            productTemplate = imageStorage.getImage(UserName, assetType, productTemplate, true);
            return View(productTemplate);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) edits. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="productTemplate">  The product template. </param>
        /// <param name="file">             The file. </param>
        ///
        /// <returns>   A response stream to send to the Edit View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: Admin/ProductTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Image,HasBackgroundImage,BackgroundImage,NumColors,Active")] ProductTemplate productTemplate, HttpPostedFileBase file)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    productTemplate.FileName = imageStorage.saveImage(UserName, assetType, file);

                } else
                {
                    productTemplate.FileName = imageStorage.saveImage(UserName, assetType, productTemplate);
                }
                db.Entry(productTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productTemplate);
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

        // GET: Admin/ProductTemplates/Delete/5
        public ActionResult Delete(long? id)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTemplate productTemplate = db.ProductTemplates.Find(id);
            if (productTemplate == null)
            {
                return HttpNotFound();
            }
            productTemplate = imageStorage.getImage(UserName, assetType, productTemplate, true);
            return View(productTemplate);
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

        // POST: Admin/ProductTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            ProductTemplate productTemplate = db.ProductTemplates.Find(id);
            imageStorage.deleteImage(UserName, assetType, productTemplate);
            db.ProductTemplates.Remove(productTemplate);
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
