using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace insomnia.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enabling cross-domain requests
            config.EnableCors();

            // Web API configuration and services
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{url}",
                defaults: new { url = RouteParameter.Optional }
            );
        }
    }
}
