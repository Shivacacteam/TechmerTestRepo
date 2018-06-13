using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechmerVision.Models;
using TechmerVision.DAL;
using TechmerVision;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using TechmerVision.Providers;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace TechmerVision.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling assets. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class AssetController : ApiController
    {
        private WorkspaceContext db = new WorkspaceContext();
        private ApplicationUserManager _userManager;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public AssetController()
        {
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="userManager">  Manager for user. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public AssetController(ApplicationUserManager userManager)
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
        /// <summary>   Action Not Implemented </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   String array stating the Asset Controller is NOT Implemented. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Asset
        public IEnumerable<string> Get()
        {
            return new string[] { "Not Implemented"};
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Action Not Implemented </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The Identifier to get. </param>
        ///
        /// <returns>   String stating the Asset Controller is NOT Implemented. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Asset/5
        public string Get(int id)
        {
            return "Not Implemented";
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Used to save a range of Assets from the Compare Screen. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <exception cref="DbUpdateConcurrencyException"> Thrown when a Database Update Concurrency
        ///                                                 error condition occurs. </exception>
        ///
        /// <param name="values">   The values. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/Asset
        public void Post(JObject[] values)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            List<Workspace> workspaces = new List<Workspace>();
            List<Grid> grids = new List<Grid>();
            List<Product> products = new List<Product>();
            List<Asset> assets = new List<Asset>();

            foreach (JObject o in values)
            {
                JToken jtoken;
                if(o.TryGetValue("userId", out jtoken)) { //Workspace
                    Workspace workspace = JsonConvert.DeserializeObject<Workspace>(o.ToString());
                    workspaces.Add(workspace);
                } else if (o.TryGetValue("internalTopLeftColorString", out jtoken)) { //Grid
                    Grid grid = JsonConvert.DeserializeObject<Grid>(o.ToString());
                    grids.Add(grid);
                    assets.Add(grid);
                }
                else if (o.TryGetValue("productTemplateId", out jtoken)) { //Product
                    Product product = JsonConvert.DeserializeObject<Product>(o.ToString());
                    products.Add(product);
                    assets.Add(product);
                    product.ProductTemplate = db.ProductTemplates.Find(product.ProductTemplate.Id);
                }
            }

            try
            {
                CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
                grids = imageStorage.saveImages(user.UserName, grids);
                products = imageStorage.saveImages(user.UserName, products);
                workspaces = imageStorage.saveImages(user.UserName, workspaces);

                foreach(Workspace workspace in workspaces) { 
                    db.Entry(workspace).State = EntityState.Modified;
                }
                foreach (Grid grid in grids)
                {
                    db.Entry(grid).State = EntityState.Modified;
                }
                foreach (Product product in products)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.Entry(product.ProductTemplate).State = EntityState.Unchanged;
                    foreach (ProductTemplateColor prodTempColor in product.ProductTemplate.ProductTemplateColors)
                    {
                        db.Entry(prodTempColor).State = EntityState.Unchanged;
                    }
                }

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (Exception e)
            {
                //TODO: Log error
                throw;
            }

        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Action NOT Implemented. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">       The Identifier to get. </param>
        /// <param name="value">    The value. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // PUT: api/Asset/5
        public void Put(int id, [FromBody]string value)
        {
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Action Not Implemented. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The Identifier to get. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // DELETE: api/Asset/5
        public void Delete(int id)
        {
        }



    }
}
