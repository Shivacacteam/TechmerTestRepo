using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TechmerVision.Models;
using Microsoft.AspNet.Identity;
using TechmerVision.DAL;
using Exceptions;
using System.Text;
using System.Data.Entity;

namespace TechmerVision.Providers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An account creation provider. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class AccountCreationProvider
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converters. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="controller">   The controller. </param>
        /// <param name="UserManager">  Manager for user. </param>
        /// <param name="invitation">   The invitation. </param>
        ///
        /// <returns>   The asynchronous result that yields an IdentityResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public async static Task<IdentityResult> Converter(Controller controller, ApplicationUserManager UserManager, Invitation invitation)
        {
            ApplicationDbContext appdb = new ApplicationDbContext();
           

            EncryptionProvider encProv = new EncryptionProvider();
            var user = new ApplicationUser
            {
                UserName = invitation.email,
                Email = invitation.email,
                FirstName = invitation.FirstName,
                LastName = invitation.LastName,
                CompanyName = invitation.CompanyName,
                Website = invitation.Website,
                Title = invitation.Title,
                Phone = invitation.Phone,
                DesignRole = invitation.DesignRole
            };
            var ret =  await Converter(controller, UserManager, user, encProv.Decrypt(invitation.password));
            invitation.status = InvitationStatus.Accepted;
            appdb.Invitations.Attach(invitation);
            appdb.Entry(invitation).State = EntityState.Modified;
            appdb.SaveChanges();
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converters. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="controller">   The controller. </param>
        /// <param name="UserManager">  Manager for user. </param>
        /// <param name="user">         The user. </param>
        /// <param name="model">        The model. </param>
        ///
        /// <returns>   The asynchronous result that yields an IdentityResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public async static Task<IdentityResult> Converter(Controller controller, ApplicationUserManager UserManager, ApplicationUser user, RegisterViewModel model)
        {
            return await Converter(controller, UserManager, user, model.Password);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converters. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <exception cref="ApplicationException"> Thrown when an Application error condition occurs. </exception>
        ///
        /// <param name="controller">   The controller. </param>
        /// <param name="UserManager">  Manager for user. </param>
        /// <param name="user">         The user. </param>
        /// <param name="password">     The password. </param>
        ///
        /// <returns>   The asynchronous result that yields an IdentityResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public async static Task<IdentityResult> Converter(Controller controller, ApplicationUserManager UserManager, ApplicationUser user, string password) { 
            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                GenerateDefaultWorkspace(user);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var callbackUrl = controller.Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: controller.Request.Url.Scheme);

                try
                {
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                }
                catch (InvalidApiRequestException ex)
                {
                    var detail = new StringBuilder();
                    detail.Append("ResponseStatusCode: " + ex.ResponseStatusCode + ".   ");
                    for (int i = 0; i < ex.Errors.Count(); i++)
                    {
                        detail.Append(" -- Error #" + i.ToString() + " : " + ex.Errors[i]);
                    }

                    throw new ApplicationException(detail.ToString(), ex);
                }
            }
            return result;
    }
        private static void AddErrors(Controller controller, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                controller.ModelState.AddModelError("", error);
            }
        }

        private static void GenerateDefaultWorkspace(ApplicationUser user)
        {
            WorkspaceContext db = new WorkspaceContext();
            String WHITECOLORSTRING = "255,255,255,1";
            Workspace initWorkspace = new Workspace();
            initWorkspace.UserId = user.Email;
            initWorkspace.s = 1;
            initWorkspace.Filename = Guid.NewGuid().ToString();
            db.Workspaces.Add(initWorkspace);

            ColorSelection initColorSelection = new ColorSelection();
            initColorSelection.WorkspaceId = initWorkspace.Id;
            initColorSelection.InternalColorString = WHITECOLORSTRING;
            initColorSelection.InternalHSL = "0,0,0";
            initColorSelection.Favorite = false;
            db.ColorSelections.Add(initColorSelection);

            for (int i = 0; i < 10; i++)
            {
                initColorSelection = new ColorSelection();
                initColorSelection.WorkspaceId = initWorkspace.Id;
                initColorSelection.InternalColorString = WHITECOLORSTRING;
                initColorSelection.InternalHSL = "0,0,0";
                initColorSelection.Favorite = true;
                db.ColorSelections.Add(initColorSelection);
            }
            db.SaveChanges();

            Grid initGrid = new Grid();
            initGrid.WorkspaceId = initWorkspace.Id;
            initGrid.Width = 10;
            initGrid.Height = 10;
            initGrid.HorizontalWeight = 1;
            initGrid.VerticalWeight = 1;
            initGrid.s = 1;
            initGrid.InternalTopLeftColorString = WHITECOLORSTRING;
            initGrid.InternalTopRightColorString = WHITECOLORSTRING;
            initGrid.InternalBottomLeftColorString = WHITECOLORSTRING;
            initGrid.InternalBottomRightColorString = WHITECOLORSTRING;
            initGrid.Filename = Guid.NewGuid().ToString();
            db.Grids.Add(initGrid);
            db.SaveChanges();
        }
    }
}