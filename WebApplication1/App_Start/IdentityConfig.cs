using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using TechmerVision.Models;
using System.Net;
using System.Configuration;
using SendGrid;

namespace TechmerVision
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An email service. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class EmailService : IIdentityMessageService
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   This method should send the message. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="message">  . </param>
        ///
        /// <returns>   The asynchronous result. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Task SendAsync(IdentityMessage message)
        {
            //Send Grid: API Token: SG.6fzFw - BMTxWifONeji2_Cw.1FEnIqjHDwhzvOm_wZwVoFAml1G9nC9RYq9Vm4Mko_0
            return configSendGridasync(message);
            // Plug in your email service here to send an email.
            //return Task.FromResult(0);
        }
        private Task configSendGridasync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(message.Destination);
            myMessage.From = new System.Net.Mail.MailAddress(
                                "TechmerVision@techmerpm.com", "Techmer Vision");
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;

            var credentials = new NetworkCredential(
                       ConfigurationManager.AppSettings["mailAccount"],
                       ConfigurationManager.AppSettings["mailPassword"]
                       );


            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            if (transportWeb != null)
            {
                return transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                return Task.FromResult(0);
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   The SMS service. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class SmsService : IIdentityMessageService
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   This method should send the message. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="message">  . </param>
        ///
        /// <returns>   The asynchronous result. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager which is used in this application.

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Manager for application users. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="store">    The store. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a new ApplicationUserManager. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="options">  Options for controlling the operation. </param>
        /// <param name="context">  The context. </param>
        ///
        /// <returns>   An ApplicationUserManager. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.  

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Manager for application sign ins. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="userManager">              Manager for user. </param>
        /// <param name="authenticationManager">    Manager for authentication. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        { }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Called to generate the ClaimsIdentity for the user, override to add additional claims
        ///     before SignIn.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="user"> . </param>
        ///
        /// <returns>   The asynchronous result that yields the new user identity asynchronous. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a new ApplicationSignInManager. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="options">  Options for controlling the operation. </param>
        /// <param name="context">  The context. </param>
        ///
        /// <returns>   An ApplicationSignInManager. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
