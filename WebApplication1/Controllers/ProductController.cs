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
    /// <summary>   A controller for handling products. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Authorize]
    public class ProductController : ApiController
    {
        private WorkspaceContext db = new WorkspaceContext();
        private ApplicationUserManager _userManager;
        private string assetType = "Products";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ProductController()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="userManager">  The user manager. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ProductController(ApplicationUserManager userManager)
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
        /// <summary>   Gets the products in this collection. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the products in this collection.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Products
        public IEnumerable<Product> GetProducts()
        {
            List<Product> ret = new List<Product>();
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);

            var user = UserManager.FindById(User.Identity.GetUserId());
            List<Product> products =  db.Products.Where(r => r.Workspace.UserId == user.UserName).OrderByDescending(r => r.ModifiedTimeStamp).ToList<Product>();

            products = imageStorage.getImages(user.UserName, assetType, products);
            //return products.ToArray();
            return products;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a product. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   The product. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(long id)
        {
            Product product = db.Products.Find(id);
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            var user = UserManager.FindById(User.Identity.GetUserId());
            product = imageStorage.getImage(user.UserName, assetType, product);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Puts a product. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <exception cref="DbUpdateConcurrencyException"> Thrown when a Database Update Concurrency
        ///                                                 error condition occurs. </exception>
        ///
        /// <param name="id">       The identifier. </param>
        /// <param name="product">  The product. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(long id, Product product)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            var user = UserManager.FindById(User.Identity.GetUserId());
            product = imageStorage.saveImage(user.UserName, assetType, product, false);

            db.Entry(product).State = EntityState.Modified;
            db.ProductColors.AddRange(product.ProductColors);
            foreach(ProductColor prodColor in product.ProductColors)
            {
                db.Entry(prodColor).State = EntityState.Modified;
            }
            db.ProductTemplates.Add(product.ProductTemplate);
            db.Entry(product.ProductTemplate).State = EntityState.Unchanged;
            db.ProductTemplateColors.AddRange(product.ProductTemplate.ProductTemplateColors);
            foreach(ProductTemplateColor prodTempColor in product.ProductTemplate.ProductTemplateColors)
            {
                db.Entry(prodTempColor).State = EntityState.Unchanged;
            }
            

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        /// <summary>   Posts a product. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="product">  The product. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            var user = UserManager.FindById(User.Identity.GetUserId());
            product = imageStorage.saveImage(user.UserName, assetType, product, true);
    
            db.ProductColors.AddRange(product.ProductColors);
            db.ProductTemplates.Add(product.ProductTemplate);
            db.Entry(product.ProductTemplate).State = EntityState.Unchanged;
            db.ProductTemplateColors.AddRange(product.ProductTemplate.ProductTemplateColors);
            foreach(ProductTemplateColor prodTempColor in product.ProductTemplate.ProductTemplateColors)
            {
                db.Entry(prodTempColor).State = EntityState.Unchanged;
            }
            
            db.Products.Add(product);
            db.SaveChanges();

            product = imageStorage.getImage(user.UserName, assetType, product);

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the product described by ID. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(long id)
        {
            Product product = db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            //var user = UserManager.FindById(User.Identity.GetUserId());
            Workspace workspace = db.Workspaces.Find(product.WorkspaceId);
            imageStorage.deleteImage(workspace.UserId, assetType, (Product) product);

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
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

        private bool ProductExists(long id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}