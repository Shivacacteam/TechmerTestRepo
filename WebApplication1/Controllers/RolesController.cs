using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechmerVision.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace TechmerVision.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling roles. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Authorize(Roles = "SysAdmin,AppAdmin")]
    public class RolesController : Controller
    {

        private ApplicationDbContext appDbContext = new ApplicationDbContext();


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the index. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the Index View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: Roles
        public ActionResult Index()
        {
            var roles = appDbContext.Roles.ToList();
            return View(roles);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) creates a new ActionResult. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the Create View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) creates a new ActionResult.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="collection">   The collection. </param>
        ///
        /// <returns>   A response stream to send to the Create View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                appDbContext.Roles.Add(new IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                appDbContext.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) edits the given role. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="roleName"> Name of the role. </param>
        ///
        /// <returns>   A response stream to send to the Edit View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            var thisRole = appDbContext.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) edits the given role. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="role"> The role. </param>
        ///
        /// <returns>   A response stream to send to the Edit View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                appDbContext.Entry(role).State = System.Data.Entity.EntityState.Modified;
                appDbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the given RoleName. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="RoleName"> Name of the role. </param>
        ///
        /// <returns>   A response stream to send to the Delete View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //DELETE: /Roles
        public ActionResult Delete(string RoleName)
        {
            var thisRole = appDbContext.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            appDbContext.Roles.Remove(thisRole);
            appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Manage user roles. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the ManageUserRoles View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            var list = appDbContext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) role add to user. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName"> Name of the user. </param>
        /// <param name="RoleName"> Name of the role. </param>
        ///
        /// <returns>   A response stream to send to the RoleAddToUser View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            ApplicationUser user = appDbContext.Users.Where(u => u.Email.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().AddToRole(user.Id, RoleName);

            ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list = appDbContext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) gets the roles. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName"> Name of the user. </param>
        ///
        /// <returns>   A response stream to send to the GetRoles View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = appDbContext.Users.Where(u => u.Email.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var account = new AccountController();           

                ViewBag.RolesForThisUser = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = appDbContext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
            }

            return View("ManageUserRoles");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) deletes the role for user. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName"> Name of the user. </param>
        /// <param name="RoleName"> Name of the role. </param>
        ///
        /// <returns>   A response stream to send to the DeleteRoleForUser View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = appDbContext.Users.Where(u => u.Email.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (UserManager.IsInRole(user.Id, RoleName))
            {
                UserManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = appDbContext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }
    }
    
}