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
using System.Web.Http;
using System.Web.Http.Description;
using TechmerVision.DAL;
using TechmerVision.Models;
using System.Security.Principal;
using System.Web;

namespace TechmerVision.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling favorite colors. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Authorize]
    public class FavoriteColorController : ApiController
    {
        private WorkspaceContext db = new WorkspaceContext();
        private ApplicationUserManager _userManager;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public FavoriteColorController()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="userManager">  The user manager. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public FavoriteColorController(ApplicationUserManager userManager)
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
        /// <summary>   Gets color selections. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   The color selections. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/FavoriteColor
        public IQueryable<ColorSelection> GetColorSelections()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            return db.ColorSelections.Where(r => r.Workspace.UserId == user.UserName && r.Favorite == true).OrderByDescending(r => r.TimeStamp);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets color selection. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   The color selection. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/FavoriteColor/5
        [ResponseType(typeof(ColorSelection))]
        public IHttpActionResult GetColorSelection(long id)
        {
            //Todo: Add Authentication
            ColorSelection colorSelection = db.ColorSelections.Find(id);
            if (colorSelection == null)
            {
                return NotFound();
            }

            return Ok(colorSelection);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Puts color selection. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <exception cref="DbUpdateConcurrencyException"> Thrown when a Database Update Concurrency
        ///                                                 error condition occurs. </exception>
        ///
        /// <param name="id">               The identifier. </param>
        /// <param name="colorSelection">   The color selection. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // PUT: api/FavoriteColor/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutColorSelection(long id, ColorSelection colorSelection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != colorSelection.Id)
            {
                return BadRequest();
            }

            db.Entry(colorSelection).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorSelectionExists(id))
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
        /// <summary>   Posts a color selection. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="colorSelection">   The color selection. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/FavoriteColor
        [ResponseType(typeof(ColorSelection))]
        public IHttpActionResult PostColorSelection(ColorSelection colorSelection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ColorSelections.Add(colorSelection);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = colorSelection.Id }, colorSelection);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the color selection described by ID. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IHttpActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // DELETE: api/FavoriteColor/5
        [ResponseType(typeof(ColorSelection))]
        public IHttpActionResult DeleteColorSelection(int id)
        {
            ColorSelection colorSelection = db.ColorSelections.Find(id);
            if (colorSelection == null)
            {
                return NotFound();
            }

            db.ColorSelections.Remove(colorSelection);
            db.SaveChanges();

            return Ok(colorSelection);
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

        private bool ColorSelectionExists(long id)
        {
            return db.ColorSelections.Count(e => e.Id == id) > 0;
        }
    }
}