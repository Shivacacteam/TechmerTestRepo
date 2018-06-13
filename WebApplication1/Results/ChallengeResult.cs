using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace TechmerVision.Results
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Encapsulates the result of a challenge. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ChallengeResult : IHttpActionResult
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="loginProvider">    The login provider. </param>
        /// <param name="controller">       The controller. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ChallengeResult(string loginProvider, ApiController controller)
        {
            LoginProvider = loginProvider;
            Request = controller.Request;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the login provider. </summary>
        ///
        /// <value> The login provider. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string LoginProvider { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the request. </summary>
        ///
        /// <value> The request. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public HttpRequestMessage Request { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Creates an <see cref="T:System.Net.Http.HttpResponseMessage" /> asynchronously.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="cancellationToken">    The token to monitor for cancellation requests. </param>
        ///
        /// <returns>
        ///     A task that, when completed, contains the
        ///     <see cref="T:System.Net.Http.HttpResponseMessage" />.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            Request.GetOwinContext().Authentication.Challenge(LoginProvider);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            return Task.FromResult(response);
        }
    }
}
