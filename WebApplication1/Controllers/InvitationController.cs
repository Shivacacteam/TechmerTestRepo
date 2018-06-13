using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechmerVision.Models;
using TechmerVision.Providers;

namespace TechmerVision.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling invitations. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [RoutePrefix("Admin/Invitations")]
    [Route("{action=index}")]
    [Authorize(Roles = "SysAdmin,AppAdmin")]
    public class InvitationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

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
        /// <summary>
        ///     (An Action that handles HTTP POST requests) (Restricted to Roles = "SysAdmin,AppAdmin)
        ///     indexes the given model.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: Admin/Invitations/Index
        public ActionResult Index()
        {
            List<Invitation> pendingInvitations = db.Invitations.Where(r => r.status == InvitationStatus.Pending).ToList();
            return View(pendingInvitations);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) (Restricted to Roles = "SysAdmin,AppAdmin)
        ///     indexes the given model.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="model">    The model. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: Admin/Invitations/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SysAdmin,AppAdmin")]
        public async Task<ActionResult> Index(List<Invitation> model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            foreach (Invitation invite in model)
            {
                switch (invite.status)
                {
                    case InvitationStatus.Accepted:
                        var result = await AccountCreationProvider.Converter(this, UserManager, invite);
                        break;
                    case InvitationStatus.Denied:
                        db.Invitations.Attach(invite);
                        db.Entry(invite).State = EntityState.Modified;
                        db.SaveChanges();
                        break;
                    default:
                        break;
                }


            }

            List<Invitation> pendingInvitations = db.Invitations.Where(r => r.status == InvitationStatus.Pending).ToList();
            return View(pendingInvitations);
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
