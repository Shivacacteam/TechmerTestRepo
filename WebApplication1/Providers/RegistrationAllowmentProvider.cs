using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechmerVision.Providers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Values that represent registration modes. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public enum RegistrationMode {
        /// <summary>   Only allow Registration if user is on the list. </summary>
        WhiteList,
        /// <summary>   Allow all registration unless user is on the list. </summary>
        BlackList,
        /// <summary>   Each Registration must be approved prior to access. </summary>
        Invitation
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A registration allowment provider. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class RegistrationAllowmentProvider
    {
        
        private RegistrationMode regMode;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public RegistrationAllowmentProvider()
        {
            regMode = RegistrationMode.Invitation;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Registration allowed. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName"> Name of the user. </param>
        ///
        /// <returns>   A RegistrationAllowmentResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public RegistrationAllowmentResult RegistrationAllowed(String UserName)
        {
            RegistrationAllowmentResult ret = new RegistrationAllowmentResult(regMode);

            switch (regMode)
            {
                case RegistrationMode.WhiteList:
                    if (inWhiteList(UserName))
                    {
                        ret.Allowed = true;
                        ret.Denied = false;
                        ret.ApprovalRequired = false;
                        //ret = true;
                    }
                    break;
                case RegistrationMode.BlackList:
                    if (inBlackList(UserName))
                    {
                        ret.Denied = true;
                        ret.Allowed = false;
                        ret.ApprovalRequired = false;
                        //ret = false;
                    }
                    break;
                case RegistrationMode.Invitation:
                    {
                        if (inBlackList(UserName))
                        {
                            ret.Denied = true;
                            ret.Allowed = false;
                            ret.ApprovalRequired = false;
                        } else if (inWhiteList(UserName))
                        {
                            ret.Allowed = true;
                            ret.Denied = false;
                            ret.ApprovalRequired = false;
                        } else
                        {
                            ret.Allowed = false;
                            ret.Denied = false;
                            ret.ApprovalRequired = true;
                        }
                        
                        break;
                    }
            }
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   In white list. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName"> Name of the user. </param>
        ///
        /// <returns>   True if it name is on the list, otherwise false </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private Boolean inWhiteList(String UserName)
        {

            if (UserName.Contains("techmerpm.com"))
                return true;
            
            if (UserName.Contains("techmeres.com"))
                return true;

            if (UserName.Contains("groupsixty.com"))
                return true;

            if (UserName.Contains("sccollective.com"))
                return true;

            if (UserName.Equals("toddwalsh@gmail.com"))
                return true;

            if (UserName.Contains("fletchermarketingpr.com"))
                return true;

            return false;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   In black list. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName"> Name of the user. </param>
        ///
        /// <returns>   True if name is blacklisted, otherwise false. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private Boolean inBlackList(String UserName)
        {
            Boolean ret = false;

            if (UserName.Equals("adam.edmonds@gmail.com"))
                return true;

            return ret;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Encapsulates the result of a registration allowment. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class RegistrationAllowmentResult
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="regMode">  The register mode. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public RegistrationAllowmentResult(RegistrationMode regMode)
        {

            switch(regMode)
            { 
                case RegistrationMode.WhiteList:
                    Allowed = false;
                    Denied = true;
                    ApprovalRequired = false;
                    Message = "Registration is only available for Techmer employees.";
                    break;
                case RegistrationMode.BlackList:
                    Allowed = true;
                    Denied = false;
                    ApprovalRequired = false;
                    Message = "Invalid email address for registration";
                    break;
                case RegistrationMode.Invitation:
                    Allowed = false;
                    Denied = false;
                    ApprovalRequired = true;
                    Message = "Registration is only available for invited parties.";
                    break;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a value indicating whether the allowed. </summary>
        ///
        /// <value> True if allowed, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Boolean Allowed { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a value indicating whether the denied. </summary>
        ///
        /// <value> True if denied, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Boolean Denied { get;  set;}

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a value indicating whether the approval required. </summary>
        ///
        /// <value> True if approval required, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Boolean ApprovalRequired { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the message. </summary>
        ///
        /// <value> The message. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public String Message { get; set; }

    }
}