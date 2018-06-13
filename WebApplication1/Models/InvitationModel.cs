using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechmerVision.Providers;

namespace TechmerVision.Models
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Values that represent invitation status. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public enum InvitationStatus
    {
        /// <summary>   An enum constant representing the pending option. </summary>
        Pending,
        /// <summary>   An enum constant representing the accepted option. </summary>
        Accepted,
        /// <summary>   An enum constant representing the denied option. </summary>
        Denied
    }
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An invitation. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Invitation
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Invitation()
        {
            status = InvitationStatus.Pending;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="registerViewModel">    The register view model. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Invitation(RegisterViewModel registerViewModel)
        {
            EncryptionProvider encProv = new EncryptionProvider();

            email = registerViewModel.Email;
            password = encProv.Encrypt(registerViewModel.Password);
            FirstName = registerViewModel.FirstName;
            LastName = registerViewModel.LastName;
            CompanyName = registerViewModel.CompanyName;
            Title = registerViewModel.Title;
            Phone = registerViewModel.Phone;
            Website = registerViewModel.Website;
            DesignRole = registerViewModel.DesignRole;
            status = InvitationStatus.Pending;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value> The identifier. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public long id { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the email. </summary>
        ///
        /// <value> The email. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Display(Name = "Email")]
        public string email { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the password. </summary>
        ///
        /// <value> The password. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Display(Name = "Password")]
        public string password { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the person's first name. </summary>
        ///
        /// <value> The name of the first. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the person's last name. </summary>
        ///
        /// <value> The name of the last. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name of the company. </summary>
        ///
        /// <value> The name of the company. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the title. </summary>
        ///
        /// <value> The title. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Display(Name = "Title")]
        public string Title { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the phone. </summary>
        ///
        /// <value> The phone. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the website. </summary>
        ///
        /// <value> The website. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Display(Name = "Website")]
        public string Website { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the design role. </summary>
        ///
        /// <value> The design role. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Display(Name = "Role in Design")]
        public string DesignRole { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the status. </summary>
        ///
        /// <value> The status. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Display(Name = "Status")]
        public InvitationStatus status {get;set;}

        

    }

}
