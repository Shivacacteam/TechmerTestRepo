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
    /// <summary>   A controller for handling sample Request. </summary>
    ///
    /// <remarks>   Shivam, 02/18/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class SampleRequestController : ApiController
    {
        private WorkspaceContext db = new WorkspaceContext();
        private ApplicationUserManager _userManager;
        private string assetType = "SampleRequest";
        private string UserName = "Techmer";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Shivam, 02/18/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public SampleRequestController()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Shivam, 02/18/2018. </remarks>
        ///
        /// <param name="userManager">  The user manager. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public SampleRequestController(ApplicationUserManager userManager)
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
        /// <summary>   Gets the sample Request in this collection. </summary>
        ///
        /// <remarks>   Shivam, 02/18/2018. </remarks>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the sample Request in this
        ///     collection.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/SampleRequests
        public List<PublicSampleRequest> GetSampleRequests()
        {
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            var user = UserManager.FindById(User.Identity.GetUserId());
            List<SampleRequest> SampleRequests = db.SampleRequest.Where(st => st.Owner == user.UserName).ToList<SampleRequest>();


            List<PublicSampleRequest> _PublicSampleRequests = new List<PublicSampleRequest>();

            #region  List Public Sample Request
            foreach (var SampleRequest in SampleRequests)
            {
                PublicSampleRequest _PublicSampleRequest = new PublicSampleRequest();

                _PublicSampleRequest.Id = SampleRequest.Id;
                _PublicSampleRequest.Owner = SampleRequest.Owner;
                _PublicSampleRequest.ProjectName = SampleRequest.ProjectName;
                _PublicSampleRequest.Notes = SampleRequest.Notes;
                _PublicSampleRequest.Status = SampleRequest.Status;
                _PublicSampleRequest.SubmissionDate = SampleRequest.SubmissionDate;
                _PublicSampleRequest.CreatedDate = SampleRequest.CreatedDate;
                _PublicSampleRequest.ModifiedDate = SampleRequest.ModifiedDate;

                //Get asset

                List<PublicRequestAsset> _PublicRequestAssets = new List<PublicRequestAsset>();
                #region  List Public Request Assets
                //foreach (SampleRequestAsset _SampleRequestAsset in SampleRequest.SampleRequestAsset.ToList()) Where(st => st.RequestId == _PublicSampleRequest.Id).
                foreach (SampleRequestAsset _SampleRequestAsset in db.SampleRequestAsset.Where(st => st.RequestId == _PublicSampleRequest.Id).ToList())
                {
                    PublicRequestAsset _PublicRequestAsset = new PublicRequestAsset();

                    switch (_SampleRequestAsset.AssetType)
                    {
                        case "Product":
                            _PublicRequestAsset.Id = _SampleRequestAsset.Id;
                            _PublicRequestAsset.AssetId = _SampleRequestAsset.AssetId;
                            _PublicRequestAsset.AssetTitle = _SampleRequestAsset.Notes;

                            Product product = db.Products.Find(_SampleRequestAsset.AssetId);
                            if (product != null)
                            {
                                string assetType = "Products";
                                product = imageStorage.getImage(user.UserName, assetType, product);
                                //product = imageStorage.getImage("Techmer", "ProductTemplates", product, true);

                                _PublicRequestAsset.Assetbackground = product.ProductTemplate.Image;
                            }
                            //_PublicRequestAsset.Assetbackground = "";
                            _PublicRequestAsset.AssetType = _SampleRequestAsset.AssetType;
                            _PublicRequestAssets.Add(_PublicRequestAsset);

                            break;
                        case "Grid":
                            Grid grid = db.Grids.Find(_SampleRequestAsset.AssetId);
                            //CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
                            //var user = UserManager.FindById(User.Identity.GetUserId());
                            grid.Image = imageStorage.getImage(user.UserName, "Grids", grid);
                            _PublicRequestAsset.AssetId = _SampleRequestAsset.AssetId;
                            _PublicRequestAsset.AssetTitle = _SampleRequestAsset.Notes;
                            _PublicRequestAsset.Assetbackground = grid.Image;
                            _PublicRequestAsset.AssetType = _SampleRequestAsset.AssetType;
                            _PublicRequestAssets.Add(_PublicRequestAsset);


                            break;
                        case "Color":
                            ColorSelection colorselection = db.ColorSelections.Find(_SampleRequestAsset.AssetId);
                            _PublicRequestAsset.AssetId = _SampleRequestAsset.AssetId;
                            _PublicRequestAsset.AssetTitle = _SampleRequestAsset.Notes;
                            _PublicRequestAsset.Assetbackground = colorselection.ColorStyle;
                            _PublicRequestAsset.AssetType = _SampleRequestAsset.AssetType;
                            _PublicRequestAssets.Add(_PublicRequestAsset);
                            break;
                    }
                }
                _PublicSampleRequest.PublicRequestAssetlist = _PublicRequestAssets;

                #endregion

                _PublicSampleRequests.Add(_PublicSampleRequest);
            }

            #endregion


            return _PublicSampleRequests;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets sample Request. </summary>
        ///
        /// <remarks>   Shivam, 02/18/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   The sample Request. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/SampleRequest/5
        [ResponseType(typeof(SampleRequest))]
        public IHttpActionResult GetSampleRequest(long id)
        {
            SampleRequest sampleRequest = db.SampleRequest.Find(id);
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);

            return Ok();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Puts sample Request. </summary>
        ///
        /// <remarks>   Shiva, 03/06/2018. </remarks>
        ///
        /// <exception cref="DbUpdateConcurrencyException"> Thrown when a Database Update Concurrency
        ///                                                 error condition occurs. </exception>
        ///
        /// <param name="id">                   The identifier. </param>
        /// <param name="sampleRequest">    The sample Request. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        //////////////////////////////////////////////////////////////////////////////////////////////////
        // PUT: api/SampleRequest/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSampleRequest(PublicSampleRequest sampleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SampleRequest _SampleRequest = db.SampleRequest.SingleOrDefault(r => r.Id == sampleRequest.Id);
                _SampleRequest.Owner = sampleRequest.Owner;
                _SampleRequest.Notes = sampleRequest.Notes;
                _SampleRequest.ProjectName = sampleRequest.ProjectName;
                _SampleRequest.Status = true;
                _SampleRequest.CreatedDate = sampleRequest.CreatedDate;
                _SampleRequest.SubmissionDate = sampleRequest.SubmissionDate;
                _SampleRequest.ModifiedDate = System.DateTime.Now;
                db.SaveChanges();
                //var id = _SampleRequest.Id;

                foreach (SampleRequestAsset _SampleRequestAsset in db.SampleRequestAsset.Where(st => st.RequestId == sampleRequest.Id).ToList())
                {
                    db.SampleRequestAsset.Remove(_SampleRequestAsset);
                    db.SaveChanges();
                }

                foreach (var item in sampleRequest.PublicRequestAssetlist)
                {
                    SampleRequestAsset _SampleRequestAsset = new SampleRequestAsset();
                    _SampleRequestAsset.RequestId = _SampleRequest.Id;
                    _SampleRequestAsset.AssetType = item.AssetType;
                    _SampleRequestAsset.Notes = item.AssetTitle;
                    _SampleRequestAsset.AssetId = item.AssetId;
                    db.SampleRequestAsset.Add(_SampleRequestAsset);
                    db.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException)
            {


            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Posts a sample inspiration. </summary>
        ///
        /// <remarks>   Shiva, 02/18/2018. </remarks>
        ///
        /// <param name="sampleInspiration">    The sample inspiration. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// POST: api/SampleRequest
        [ResponseType(typeof(PublicSampleRequest))]
        public IHttpActionResult PostSampleRequest(PublicSampleRequest sampleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //create//
                SampleRequest _SampleRequest = new SampleRequest();
                _SampleRequest.Owner = sampleRequest.Owner;
                _SampleRequest.Notes = sampleRequest.Notes;
                _SampleRequest.ProjectName = sampleRequest.ProjectName;
                _SampleRequest.Status = false;
                _SampleRequest.CreatedDate = System.DateTime.Now;
                _SampleRequest.SubmissionDate = System.DateTime.Now;
                _SampleRequest.ModifiedDate = System.DateTime.Now;
                db.SampleRequest.Add(_SampleRequest);
                db.SaveChanges();
                var id = _SampleRequest.Id;
                foreach (var item in sampleRequest.PublicRequestAssetlist)
                {
                    SampleRequestAsset _SampleRequestAsset = new SampleRequestAsset();
                    _SampleRequestAsset.RequestId = _SampleRequest.Id;
                    _SampleRequestAsset.AssetType = item.AssetType;
                    _SampleRequestAsset.Notes = item.AssetTitle;
                    _SampleRequestAsset.AssetId = item.AssetId;
                    db.SampleRequestAsset.Add(_SampleRequestAsset);
                    db.SaveChanges();
                }
            }

            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = sampleRequest.Id }, sampleRequest);
            //sampleInspiration.Image = imageStorage.getImage(UserName, assetType, sampleInspiration);

            //return CreatedAtRoute("DefaultApi", new { id = sampleInspiration.Id }, sampleInspiration);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the sample Request described by ID. </summary>
        ///
        /// <remarks>   Shivam, 02/18/2018. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // DELETE: api/SampleRequest/5
        [ResponseType(typeof(SampleRequest))]
        public IHttpActionResult DeleteSampleRequestAsset(long id)
        {
            SampleRequest _SampleRequest = db.SampleRequest.SingleOrDefault(r => r.Id == id);
            if (_SampleRequest == null)
            {
                return NotFound();
            }

            db.SampleRequest.Remove(_SampleRequest);
            db.SaveChanges();
            foreach (SampleRequestAsset _SampleRequestAsset in db.SampleRequestAsset.Where(st => st.RequestId == id).ToList())
            {
                db.SampleRequestAsset.Remove(_SampleRequestAsset);
                db.SaveChanges();
            }

            return Ok(_SampleRequest);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Releases the unmanaged resources that are used by the object and, optionally, releases
        ///     the managed resources.
        /// </summary>
        ///
        /// <remarks>   Shivam, 02/18/2017. </remarks>
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