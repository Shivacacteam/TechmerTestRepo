using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using TechmerVision.Policy;

namespace TechmerVision
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A web API configuration. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public static class WebApiConfig
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Registers this object. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="config">   The configuration. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            //Enable CORS
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //var cors = new webApiCorsPolicyAttribute();
            var cors = new EnableCorsAttribute(
                origins: "*",
                headers: "*",
                methods: "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );  
        }
    }
}
