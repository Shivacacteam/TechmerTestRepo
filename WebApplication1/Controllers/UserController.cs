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
    /// <summary>   A controller for handling users. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [RoutePrefix("Admin/Users")]
    [Route("{action=index}")]
    [Authorize(Roles = "SysAdmin,AppAdmin")]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;


        // GET: Admin/Users/Index

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the index. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the Index View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult Index()
        {
            return View(db.Users.ToList());
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
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /*
        // GET: Admin/Users/Details/5
        public ActionResult ApplicationUserDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }
        */


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) creates a new Task&lt;ActionResult&gt;
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) creates a new Task&lt;ActionResult&gt;
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="applicationUser">  The application user. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: Admin/Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FirstName,LastName,CompanyName,Title,Phone,Website,DesignRole,Email,EmailConfirmed")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                applicationUser.UserName = applicationUser.Email;
                string password = "Temp1234!";
                //TODO: Generate Default Password
                var result = await AccountCreationProvider.Converter(this, UserManager, applicationUser, password);
                if(result.Errors.Count() > 0) {
                    // If we got this far, something failed, redisplay form
                    AddErrors(result);
                    DisplayErrorMessage();
                    return View(applicationUser);
                }

                //Sucess Return to Index
                return RedirectToAction("Index");
            }
            DisplayErrorMessage();
            return View(applicationUser);

        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) edits the given application user.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   A response stream to send to the Edit View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) edits the given application user.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="applicationUser">  The application user. </param>
        ///
        /// <returns>   A response stream to send to the Edit View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Claims,Logins,Roles,Id,Hometown,FirstName,LastName,CompanyName,Title,Phone,Website,DesignRole,CreatedDateTime,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a ApplicationUser record");
                return RedirectToAction("Index");
            }
            DisplayErrorMessage();
            return View(applicationUser);
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

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) (Defines the UserDelete Action) deletes the
        ///     confirmed described by ID.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   A response stream to send to the DeleteConfirmed View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            //TODO:Delete Assets
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            DisplaySuccessMessage("Has deleted a User record");
            return RedirectToAction("Index");
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) (Defines the ResetPassword Action) resets the
        ///     password described by ID.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: Admin/Users/ResetPassword/5
        [HttpPost, ActionName("ResetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            // Send an email with this link
            string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(user.Id, "Password Reset", "The Administrator has reset your password.  To setup a new password, click <a href=\"" + callbackUrl + "\">here</a>");
            DisplaySuccessMessage(String.Concat("Password reset email has been sent to ", user.Email, "."));
            return RedirectToAction("Index");
        }

        private void DisplaySuccessMessage(string msgText)
        {
            TempData["SuccessMessage"] = msgText;
        }

        private void DisplayErrorMessage()
        {
            TempData["ErrorMessage"] = "Save changes was unsuccessful.";
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
