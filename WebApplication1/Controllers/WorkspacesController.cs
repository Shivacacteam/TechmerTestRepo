using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
using System.Security.Principal;
using System;
using System.Collections.Generic;
using TechmerVision.Providers;
using System.Web.Http.Cors;

namespace TechmerVision.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling workspaces.10. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Authorize]
    public class WorkspacesController : ApiController
    {
        private WorkspaceContext db = new WorkspaceContext();
        private ApplicationUserManager _userManager;
        private string assetType = "Workspaces";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public WorkspacesController()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="userManager">  The user manager. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public WorkspacesController(ApplicationUserManager userManager)
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
        /// <summary>   Gets the workspaces in this collection. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the workspaces in this collection.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Workspaces
        public IEnumerable<Workspace> GetWorkspaces()
        {
            List<Workspace> ret = new List<Workspace>();
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            var UserId = User.Identity.GetUserId();
            var user = UserManager.FindById(UserId);
            List<Workspace> workspaces = db.Workspaces.Where(ws => ws.UserId == User.Identity.Name).ToList<Workspace>();

            ret = imageStorage.getImages(user.UserName, assetType, workspaces);
            return ret.OrderByDescending(r => r.ModifiedTimeStamp).ToArray();

        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a workspace. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   The workspace. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Workspaces/5
        [ResponseType(typeof(Workspace))]
        public IHttpActionResult GetWorkspace(int id)
        {
            Workspace workspace = db.Workspaces.Find(id);
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            var user = UserManager.FindById(User.Identity.GetUserId());
            workspace.Image = imageStorage.getImage(user.UserName, assetType, workspace);
            if (workspace == null)
            {
                return NotFound();
            }

            if (!Authorized(User, workspace))
            {
                return Unauthorized();
            }

            return Ok(workspace);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Puts a workspace. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <exception cref="DbUpdateConcurrencyException"> Thrown when a Database Update Concurrency
        ///                                                 error condition occurs. </exception>
        ///
        /// <param name="id">           The identifier. </param>
        /// <param name="workspace">    The workspace. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // PUT: api/Workspaces/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkspace(long id, Workspace workspace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workspace.Id)
            {
                return BadRequest();
            }

            if (!Authorized(User, workspace))
            {
                return Unauthorized();
            }

            
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            var user = UserManager.FindById(User.Identity.GetUserId());
            workspace.Filename = imageStorage.saveImage(user.UserName, assetType, workspace, false);

            db.Entry(workspace).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkspaceExists(id))
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
        /// <summary>   Posts a workspace. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="workspace">    The workspace. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/Workspaces
        [ResponseType(typeof(Workspace))]
        public IHttpActionResult PostWorkspace(Workspace workspace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!Authorized(User, workspace))
            {
                return Unauthorized();
            }

            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            var user = UserManager.FindById(User.Identity.GetUserId());

            if (workspace.Image != "")
            {
                workspace.Filename = imageStorage.saveImage(user.UserName, assetType, workspace, true);
            }

            db.Workspaces.Add(workspace);
            db.SaveChanges();
            workspace.Image = imageStorage.getImage(user.UserName, assetType, workspace);

            return CreatedAtRoute("DefaultApi", new { id = workspace.Id }, workspace);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the workspace described by ID. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // DELETE: api/Workspaces/5
        [ResponseType(typeof(Workspace))]
        public IHttpActionResult DeleteWorkspace(long id)
        {
            Workspace workspace = db.Workspaces.Find(id);
            if (workspace == null)
            {
                return NotFound();
            }

            if (!Authorized(User, workspace))
            {
                return Unauthorized();
            }

            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            //var user = UserManager.FindById(User.Identity.GetUserId());
            imageStorage.deleteImage(workspace.UserId, assetType, workspace);

            db.Workspaces.Remove(workspace);
            db.SaveChanges();

            return Ok(workspace);
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

        private bool WorkspaceExists(long id)
        {
            return db.Workspaces.Count(e => e.Id == id) > 0;
        }

        private bool Authorized(IPrincipal User, Workspace workspace)
        {
            if (workspace.UserId == User.Identity.Name)
            {
                return true;
            }
            
            return false;
        }
    }
}