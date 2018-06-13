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
    /// <summary>   A controller for handling grids. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Authorize]
    public class GridsController : ApiController
    {
        private WorkspaceContext db = new WorkspaceContext();
        private ApplicationUserManager _userManager;
        private string assetType = "Grids";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public GridsController()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="userManager">  The user manager. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public GridsController(ApplicationUserManager userManager)
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
        /// <summary>   Gets the grids in this collection. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the grids in this collection.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Grids
        public IEnumerable<Grid> GetGrids()
        {
            List<Grid> ret = new List<Grid>();
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);

            //Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            var user = UserManager.FindById(User.Identity.GetUserId());
            List<Grid> grids =  db.Grids.Where(r => r.Workspace.UserId == user.UserName).ToList<Grid>();
            LoadGridArrays(ref grids);
            ret = imageStorage.getImages(user.UserName, assetType, grids);

            return ret.OrderByDescending(r => r.ModifiedTimeStamp).ToArray();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a grid. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   The grid. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Grids/5
        [ResponseType(typeof(Grid))]
        public IHttpActionResult GetGrid(int id)
        {
            Grid grid = db.Grids.Find(id);
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            var user = UserManager.FindById(User.Identity.GetUserId());
            grid.Image = imageStorage.getImage(user.UserName, assetType, grid);

            if (grid == null)
            {
                return NotFound();
            }

            return Ok(grid);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Puts a grid. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <exception cref="DbUpdateConcurrencyException"> Thrown when a Database Update Concurrency
        ///                                                 error condition occurs. </exception>
        ///
        /// <param name="id">   The identifier. </param>
        /// <param name="grid"> The grid. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // PUT: api/Grids/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGrid(long id, Grid grid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grid.Id)
            {
                return BadRequest();
            }

            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            var user = UserManager.FindById(User.Identity.GetUserId());
            grid.Filename = imageStorage.saveImage(user.UserName, assetType, grid, false);

            db.Entry(grid).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GridExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
            //return Ok(grid);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Posts a grid. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="grid"> The grid. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/Grids
        [ResponseType(typeof(Grid))]
        public IHttpActionResult PostGrid(Grid grid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            var user = UserManager.FindById(User.Identity.GetUserId());
            grid.Filename = imageStorage.saveImage(user.UserName, assetType, grid, true);

            db.Grids.Add(grid);
            db.SaveChanges();
            grid.Image = imageStorage.getImage(user.UserName, assetType, grid);

            return CreatedAtRoute("DefaultApi", new { id = grid.Id }, grid);
        }
        

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the grid described by ID. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // DELETE: api/Grids/5
        [ResponseType(typeof(Grid))]
        public IHttpActionResult DeleteGrid(long id)
        {
            Grid grid = db.Grids.Find(id);

            if (grid == null)
            {
                return NotFound();
            }

            CloudStorageProvider imageStorage = new CloudStorageProvider(UserManager);
            //var user = UserManager.FindById(User.Identity.GetUserId());
            Workspace workspace = db.Workspaces.Find(grid.WorkspaceId);
            imageStorage.deleteImage(workspace.UserId, assetType, grid);

            db.Grids.Remove(grid);
            db.SaveChanges();

            return Ok(grid);
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

        private bool GridExists(long id)
        {
            return db.Grids.Count(e => e.Id == id) > 0;
        }

        private void LoadGridArrays(ref List<Grid> grids)
        {
            for (int i = 0; i < grids.Count; i++)
            {
                Grid grid = grids[i];
                grid.GridArray = _initGridArray(grid.Width, grid.Height);
                _populateCorners(ref grid, grid.TopLeftColorData, grid.TopRightColorData, grid.BottomLeftColorData, grid.BottomRightColorData);
                _loadCol(ref grid, 0);
                _loadCol(ref grid, grid.Width - 1);

                for (int j = 0; j < grid.Height; j++)
                {
                    _loadRow(ref grid, grid.GridArray[j]);
                }
            }
        }

        private void _loadRow (ref Grid grid, Color[] array) {
            for (var i = 1; i<grid.Width - 1; i++) {
                var hw = grid.HorizontalWeight;
                var stepValue = ((i * (hw)) / (grid.Width - 1));
                var ret = _lerp(array[0].ColorData.ToArray<int>(), array[grid.Width - 1].ColorData.ToArray<int>(), stepValue); //v1
                if (ret.Length == 3) {
                    int[] temp1 = new int[1];
                    temp1[1] = 1;
                    ret.Concat(temp1); //alpha
                }
                array[i] = new Color(ret);
            }
        }
        private void _loadCol(ref Grid grid, int col)
        {
            for (var ii = 1; ii < grid.Height - 1; ii++)
            {
                decimal stepValue = ((ii * (grid.VerticalWeight)) / (grid.Height - 1));
                int[] ret = _lerp(grid.GridArray[0][col].ColorData.ToArray<int>(), grid.GridArray[grid.Height - 1][col].ColorData.ToArray<int>(), stepValue);
                if (ret.Length == 3)
                {
                    int[] temp1 = new int[1];
                    temp1[1] = 1;
                    ret.Concat (temp1); //alpha
                }
                grid.GridArray[ii][col] = new Color(ret);
            }
        }

        private int[] _lerp(int[] a, int[] b, decimal fac)
        {
            int[] lerpRet = a;
            for (int colorVal = 0; colorVal < a.Length; colorVal++)
            {
                var temp = a[colorVal] * (1 - fac) + b[colorVal] * fac;
                lerpRet[colorVal] = (int) Math.Round(Math.Max(Math.Min(temp, 255), 0));
            }
            return lerpRet;
        }
        private Color[][] _initGridArray(int width, int height) {
            List<Color[]> ret = new List<Color[]>();
            for (var i = 0; i<height; i++) {
                List<Color> tempRow = new List<Color>();
                for (var j = 0; j<width; j++) {
                    int[] colorNums = { 255, 255, 255, 1 };
                    tempRow.Add(new Color(colorNums));
                }
                ret.Add(tempRow.ToArray());
            }
            return ret.ToArray();
        }

        private void  _populateCorners(ref Grid grid, int[] TopLeft, int[] TopRight, int[] BottomLeft, int[] BottomRight) {
            grid.GridArray[0][0] = new Color(TopLeft);
            grid.GridArray[0][grid.Width - 1] = new Color(TopRight);
            grid.GridArray[grid.Height - 1][0] = new Color(BottomLeft);
            grid.GridArray[grid.Height - 1][grid.Width - 1] = new Color(BottomRight);
        }
    }
}