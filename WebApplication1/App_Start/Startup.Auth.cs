using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Twitter;
using Owin;
using TechmerVision.Models;
using TechmerVision.Providers;
using Microsoft.Owin.Security;

namespace TechmerVision
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A startup. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class Startup
    {
        // Enable the application to use OAuthAuthorization. You can then secure your Web APIs

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Static constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        static Startup()
        {
            
            PublicClientId = "web";

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                AuthorizeEndpointPath = new PathString("/Account/Authorize"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets options for controlling the authentication. </summary>
        ///
        /// <value> Options that control the authentication. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the identifier of the public client. </summary>
        ///
        /// <value> The identifier of the public client. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Configure authentication. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="app">  The application. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(20),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            app.UseMicrosoftAccountAuthentication(
                clientId: "000000004818E6A4",
                clientSecret: "QUCoEoyZ6nYTw0pZyUbKozFBkwjlvgzn");

            //app.UseTwitterAuthentication(
            //    consumerKey: "50bgdOBzPUdfdUdz9gBxsj57u",
            //    consumerSecret: "5OBfxU4USS08ZXEzXRNEcZZguADad1qxjG3F0DrD8nxqIlfLWX"
                
            //    );
            app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            {
                ConsumerKey = "50bgdOBzPUdfdUdz9gBxsj57u",
                ConsumerSecret = "5OBfxU4USS08ZXEzXRNEcZZguADad1qxjG3F0DrD8nxqIlfLWX",
                BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(new[]
                {
                    "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
                    "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
                    "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
                    "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
                    "5168FF90AF0207753CCCD9656462A212B859723B", //DigiCert SHA2 High Assurance Server C‎A 
                    "B13EC36903F8BF4701D498261A0802EF63642BC3" //DigiCert High Assurance EV Root CA
                })
            });
            app.UseFacebookAuthentication(
                appId: "1721939471382143",
                appSecret: "5791fe289935183652592a03276e47b1");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "421974538452-c7cvqkprmt2apgjov03412gq4lr6f382.apps.googleusercontent.com",
                ClientSecret = "F9IhmKJMqSrfIwGC80FB0XBv"
            });
        }
    }
}
