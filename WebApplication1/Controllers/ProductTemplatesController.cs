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
using TechmerVision.DAL;
using TechmerVision.Models;
using TechmerVision.Providers;

namespace TechmerVision.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling product templates. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ProductTemplatesController : ApiController
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the product templates in this collection. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the product templates in this
        ///     collection.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/ProductTemplates
        public IEnumerable<ProductTemplate> GetProductTemplates()
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            //Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            List<ProductTemplate> productTemplates =  db.ProductTemplates.Where(pt => pt.Active == true).ToList();
            productTemplates = imageStorage.getImages(UserName, assetType, productTemplates, true);
            return productTemplates.ToArray();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets product template. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   The product template. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/ProductTemplates/5
        [ResponseType(typeof(ProductTemplate))]
        public IHttpActionResult GetProductTemplate(long id)
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            ProductTemplate productTemplate = db.ProductTemplates.Find(id);
            if (productTemplate == null || productTemplate.Active == false)
            {
                return NotFound();
            }
            productTemplate = imageStorage.getImage(UserName, assetType, productTemplate, true);
            return Ok(productTemplate);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Puts product template. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <exception cref="DbUpdateConcurrencyException"> Thrown when a Database Update Concurrency
        ///                                                 error condition occurs. </exception>
        ///
        /// <param name="id">               The identifier. </param>
        /// <param name="productTemplate">  The product template. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // PUT: api/ProductTemplates/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductTemplate(long id, ProductTemplate productTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productTemplate.Id)
            {
                return BadRequest();
            }

            db.Entry(productTemplate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTemplateExists(id))
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
        /// <summary>   Posts a product template. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="productTemplate">  The product template. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/ProductTemplates
        [ResponseType(typeof(ProductTemplate))]
        public IHttpActionResult PostProductTemplate(ProductTemplate productTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            productTemplate.FileName = imageStorage.saveImage(UserName, assetType, productTemplate);
           
            db.ProductTemplates.Add(productTemplate);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = productTemplate.Id }, productTemplate);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the product template described by ID. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // DELETE: api/ProductTemplates/5
        [ResponseType(typeof(ProductTemplate))]
        public IHttpActionResult DeleteProductTemplate(long id)
        {
            ProductTemplate productTemplate = db.ProductTemplates.Find(id);
            if (productTemplate == null)
            {
                return NotFound();
            }
            
            db.ProductTemplates.Remove(productTemplate);
            db.SaveChanges();

            return Ok(productTemplate);
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

        private bool ProductTemplateExists(long id)
        {
            return db.ProductTemplates.Count(e => e.Id == id) > 0;
        }
    }
}