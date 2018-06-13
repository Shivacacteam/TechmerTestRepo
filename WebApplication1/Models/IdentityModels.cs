using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace TechmerVision.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An application user. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ApplicationUser : IdentityUser
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the hometown. </summary>
        ///
        /// <value> The hometown. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Hometown { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the person's first name. </summary>
        ///
        /// <value> The name of the first. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string FirstName { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the person's last name. </summary>
        ///
        /// <value> The name of the last. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string LastName { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name of the company. </summary>
        ///
        /// <value> The name of the company. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string CompanyName { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the title. </summary>
        ///
        /// <value> The title. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Title { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the phone. </summary>
        ///
        /// <value> The phone. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Phone { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the website. </summary>
        ///
        /// <value> The website. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Website { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the design role. </summary>
        ///
        /// <value> The design role. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string DesignRole { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the created date time. </summary>
        ///
        /// <value> The created date time. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Column(TypeName = "DateTime2")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDateTime { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Generates a user identity asynchronous. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="manager">  The manager. </param>
        ///
        /// <returns>   The asynchronous result that yields the user identity asynchronous. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Generates a user identity asynchronous. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="manager">              The manager. </param>
        /// <param name="authenticationType">   Type of the authentication. </param>
        ///
        /// <returns>   The asynchronous result that yields the user identity asynchronous. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An application database context. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a new ApplicationDbContext. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   An ApplicationDbContext. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the invitations. </summary>
        ///
        /// <value> The invitations. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DbSet<Invitation> Invitations { get; set; }

    }
}