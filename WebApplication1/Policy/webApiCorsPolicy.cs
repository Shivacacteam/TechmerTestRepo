using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace TechmerVision.Policy
{


    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Attribute for web API cors policy. 
    ///             Reference: https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api
    ///             </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class webApiCorsPolicyAttribute : Attribute, ICorsPolicyProvider
    {
        private CorsPolicy _policy;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public webApiCorsPolicyAttribute()
        {
            // Create a CORS policy.
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };
            _policy.AllowAnyOrigin = true;

            // Add allowed origins.
            //_policy.Origins.Add("https://test-colorproject-preview.azurewebsites.net/");
            //_policy.Origins.Add("https://techmervision.com");
            //_policy.Origins.Add("http://127.0.0.1:8080/");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the <see cref="T:System.Web.Cors.CorsPolicy" />. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="request">  The request. </param>
        ///
        /// <returns>   The <see cref="T:System.Web.Cors.CorsPolicy" />. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request)
        {
            return Task.FromResult(_policy);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the <see cref="T:System.Web.Cors.CorsPolicy" />. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="request">              The request. </param>
        /// <param name="cancellationToken">    The cancellation token. </param>
        ///
        /// <returns>   The <see cref="T:System.Web.Cors.CorsPolicy" />. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_policy);
        }
    }
}