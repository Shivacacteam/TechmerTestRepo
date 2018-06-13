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
    /// <summary>   A controller for handling admin product template colors. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [RoutePrefix("Admin/ProductTemplateColor")]
    [Route("{action=index}")]
    public class AdminProductTemplateColorsController : Controller
    {
        private WorkspaceContext db = new WorkspaceContext();
        private ApplicationUserManager _userManager;
        private string assetType = "ProductTemplateColors";
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

        // GET: AdminProductTemplateColors
        public ActionResult Index()
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            
            var productTemplateColors = db.ProductTemplateColors.Include(p => p.ProductTemplate).ToList();
            productTemplateColors = imageStorage.getImages(UserName, assetType, productTemplateColors);
            return View(productTemplateColors.ToList());
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

        // GET: AdminProductTemplateColors/Details/5
        public ActionResult Details(long? id)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTemplateColor productTemplateColor = db.ProductTemplateColors.Find(id);
            if (productTemplateColor == null)
            {
                return HttpNotFound();
            }
            productTemplateColor = imageStorage.getImage(UserName, assetType, productTemplateColor);
            return View(productTemplateColor);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) creates a new ActionResult. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="productTemplateid">    (Optional) The product templateid. </param>
        ///
        /// <returns>   A response stream to send to the Create View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: AdminProductTemplateColors/Create
        public ActionResult Create(int productTemplateid = -1)
        {
            
            ViewBag.ProductTemplateId = new SelectList(db.ProductTemplates, "Id", "Title", productTemplateid);
            return View();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) creates a new ActionResult.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="productTemplateColor"> The product template color. </param>
        /// <param name="file">                 The file. </param>
        ///
        /// <returns>   A response stream to send to the Create View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: AdminProductTemplateColors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductTemplateId,ColorNumber,InternalColorString,RecolorToleranceUpperLimit,RecolorToleranceLowerLimit,Image")] ProductTemplateColor productTemplateColor, HttpPostedFileBase file)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            if (ModelState.IsValid)
            {
                if(file != null && file.ContentLength > 0) {
                    productTemplateColor.FileName = imageStorage.saveImage(UserName, assetType, file);
                    db.ProductTemplateColors.Add(productTemplateColor);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Details", "AdminProductTemplates", new { id = productTemplateColor.ProductTemplateId });
                }
            }

            ViewBag.ProductTemplateId = new SelectList(db.ProductTemplates, "Id", "Title", productTemplateColor.ProductTemplateId);
            return View(productTemplateColor);
            //return RedirectToAction("Details", "AdminProductTemplates",new { id = productTemplateColor.ProductTemplateId });
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

        // GET: AdminProductTemplateColors/Edit/5
        public ActionResult Edit(long? id)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTemplateColor productTemplateColor = db.ProductTemplateColors.Find(id);
            if (productTemplateColor == null)
            {
                return HttpNotFound();
            }
            productTemplateColor = imageStorage.getImage(UserName, assetType, productTemplateColor);
            ViewBag.ProductTemplateId = new SelectList(db.ProductTemplates, "Id", "Title", productTemplateColor.ProductTemplateId);
            return View(productTemplateColor);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) edits. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="productTemplateColor"> The product template color. </param>
        /// <param name="file">                 The file. </param>
        ///
        /// <returns>   A response stream to send to the Edit View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: AdminProductTemplateColors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductTemplateId,ColorNumber,InternalColorString,RecolorToleranceUpperLimit,RecolorToleranceLowerLimit,Image")] ProductTemplateColor productTemplateColor, HttpPostedFileBase file)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    productTemplateColor.FileName = imageStorage.saveImage(UserName, assetType, file);
                } else
                {
                    productTemplateColor.FileName = imageStorage.saveImage(UserName, assetType, productTemplateColor);
                }
                db.Entry(productTemplateColor).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", "AdminProductTemplates", new { id = productTemplateColor.ProductTemplateId });
            }
            ViewBag.ProductTemplateId = new SelectList(db.ProductTemplates, "Id", "Title", productTemplateColor.ProductTemplateId);
            return View(productTemplateColor);
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

        // GET: AdminProductTemplateColors/Delete/5
        public ActionResult Delete(long? id)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTemplateColor productTemplateColor = db.ProductTemplateColors.Find(id);
            productTemplateColor = imageStorage.getImage(UserName, assetType, productTemplateColor);
            if (productTemplateColor == null)
            {
                return HttpNotFound();
            }
            return View(productTemplateColor);
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

        // POST: AdminProductTemplateColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            ProductTemplateColor productTemplateColor = db.ProductTemplateColors.Find(id);
            imageStorage.deleteImage(UserName, assetType, productTemplateColor);
            db.ProductTemplateColors.Remove(productTemplateColor);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("Details", "AdminProductTemplate", new { id = productTemplateColor.ProductTemplateId });
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
