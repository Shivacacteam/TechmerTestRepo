using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TechmerVision.Models;
using TechmerVision.DAL;
using TechmerVision.Providers;
using Exceptions;
using System.Text;
using System.Net;

namespace TechmerVision.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling accounts. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext appDb = new ApplicationDbContext();
        private WorkspaceContext db = new WorkspaceContext();
        private const Boolean INVITATIONSINUSE = true;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public AccountController()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="userManager">      The user manager. </param>
        /// <param name="signInManager">    The sign in manager. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the manager for sign in. </summary>
        ///
        /// <value> The sign in manager. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP GET requests) gets the authorize. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the Authorize View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // The Authorize Action is the end point which gets called when you access any
        // protected Web API. If the user is not logged in then they will be redirected to 
        // the Login page. After a successful login you can call a Web API.
        [HttpGet]
        public ActionResult Authorize()
        {
            var claims = new ClaimsPrincipal(User).Claims.ToArray();
            var identity = new ClaimsIdentity(claims, "Bearer");
            AuthenticationManager.SignIn(identity);
            return new EmptyResult();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) login. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="returnUrl">    URL of the return. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) login. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="model">        The model. </param>
        /// <param name="returnUrl">    URL of the return. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

          //var newPasswordHash = UserManager.PasswordHasher.HashPassword("SuperDuper@1");

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    if(returnUrl == null)
                    {
                        returnUrl = "/App";
                    }
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) verify code. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="provider">     The provider. </param>
        /// <param name="returnUrl">    URL of the return. </param>
        /// <param name="rememberMe">   True to remember me. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) verify code. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="model">    The model. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) registers this object. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) registers this object. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="model">    The model. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CompanyName = model.CompanyName,
                    Website = model.Website,
                    Title = model.Title,
                    Phone = model.Phone,
                    DesignRole = model.DesignRole
                };
                var passwordValid = await UserManager.PasswordValidator.ValidateAsync(model.Password);
                
                
                RegistrationAllowmentProvider regProv = new RegistrationAllowmentProvider();
                RegistrationAllowmentResult regResult = regProv.RegistrationAllowed(user.Email);
                if (regResult.Denied)
                {
                    var result = new IdentityResult(new string[] { "Registration is only available for Techmer employees." });
                    AddErrors(result);
                }
                else if (!captchaValid())
                {
                    var result = new IdentityResult(new string[] { "reCaptcha validation failed." });
                    AddErrors(result);
                } else if (!passwordValid.Succeeded)
                {
                    AddErrors(passwordValid);
                } else if (regResult.ApprovalRequired)
                {
                    Invitation invite = new Invitation(model);

                    if (appDb.Invitations.Where(i => i.email == model.Email).Count() > 0)
                    {
                        var result = new IdentityResult(new String[] { "Request for access already exists for this email address." });
                        AddErrors(result);
                    }
                    else if (appDb.Users.Where(i => i.Email == model.Email).Count() > 0)
                    {
                        var result = new IdentityResult(new String[] { "A user account already exists for this email address." });
                        AddErrors(result);

                    }
                    else
                    {
                        appDb.Invitations.Add(invite);
                        appDb.SaveChanges();

                        return View("InvitationReceived");
                    }
                } else if (regResult.Allowed) { 
                    var result = await AccountCreationProvider.Converter(this, UserManager, user, model);
                    if (result.Succeeded)
                    {
                        return View("DisplayEmail");
                    }
                    else { 
                        AddErrors(result);
                    }
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void GenerateDefaultWorkspace(ApplicationUser user)
        {
            String WHITECOLORSTRING = "255,255,255,1";
            Workspace initWorkspace = new Workspace();
            initWorkspace.UserId = user.Email;
            initWorkspace.s = 1;
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
            db.Grids.Add(initGrid);
            db.SaveChanges();
        }

        private bool captchaValid()
        {
            string Response = Request["g-recaptcha-response"];
            bool Valid = false;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret=6LchRhITAAAAAIipGtL53NS-_c2h7R9Gqqdthl8n&response=" + Response);
            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (System.IO.StreamReader readStream = new System.IO.StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        reCaptchaResponse data = js.Deserialize<reCaptchaResponse>(jsonResponse);

                        Valid = Convert.ToBoolean(data.success);
                    }
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
            return Valid;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Confirm email. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="userId">   Identifier for the user. </param>
        /// <param name="code">     The code. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            var user = UserManager.FindById(userId);
            if (result.Succeeded)
            {
                
            }
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) forgot password. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) forgot password. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="model">    The model. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Forgot password confirmation. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the ForgotPasswordConfirmation View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) resets the password described by model.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="code"> The code. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) resets the password described by model.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="model">    The model. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Resets the password confirmation. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the ResetPasswordConfirmation View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) external login. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="provider">     The provider. </param>
        /// <param name="returnUrl">    URL of the return. </param>
        ///
        /// <returns>   A response stream to send to the ExternalLogin View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) sends a code. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="returnUrl">    URL of the return. </param>
        /// <param name="rememberMe">   True to remember me. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) sends a code. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="model">    The model. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Callback, called when the external login. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="returnUrl">    URL of the return. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    if (returnUrl == null)
                    {
                        returnUrl = "/App";
                    }
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    if (INVITATIONSINUSE)
                    {
                        return View("Register", new RegisterViewModel { Email = loginInfo.Email });

                    } else { 
                        // If the user does not have an account, then prompt the user to create an account
                        ViewBag.ReturnUrl = returnUrl;
                        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel {
                            Email = loginInfo.Email
                        });
                    }
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     (An Action that handles HTTP POST requests) external login confirmation.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="model">        The model. </param>
        /// <param name="returnUrl">    URL of the return. </param>
        ///
        /// <returns>   The asynchronous result that yields an ActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser {
                    UserName = model.Email, Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CompanyName = model.CompanyName,
                    Title = model.Title,
                    Website = model.Website,
                    Phone = model.Phone,
                    DesignRole = model.DesignRole
                };


                RegistrationAllowmentProvider regProv = new RegistrationAllowmentProvider();
                RegistrationAllowmentResult regResult = regProv.RegistrationAllowed(user.Email);
                if (regResult.Denied || regResult.ApprovalRequired)
                {
                    var identResult = new IdentityResult(new string[] { "Registration is only available for Techmer employees." });
                    AddErrors(identResult);
                } else if (regResult.Allowed)
                {
                    var result = await UserManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        GenerateDefaultWorkspace(user);

                        result = await UserManager.AddLoginAsync(user.Id, info.Login);
                        if (result.Succeeded)
                        {
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            return RedirectToLocal(returnUrl);
                        }
                    }
                    AddErrors(result);
                } 
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) log off. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the LogOff View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   External login failure. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A response stream to send to the ExternalLoginFailure View. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
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
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }

                if(appDb != null)
                {
                    appDb.Dispose();
                    appDb = null;
                }
                if(db != null)
                {
                    db.Dispose();
                    db = null;
                }

            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
