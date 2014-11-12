using insomnia.Domain;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace insomnia.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Unity
            var unity = new UnityContainer();
            unity.RegisterType<IRequestEngine, RequestEngine>();
            config.DependencyResolver = new UnityResolver(unity);

            // Tracing
            config.EnableSystemDiagnosticsTracing();

            // Enabling cross-domain requests. For my sites and local development.
            var cors = new EnableCorsAttribute("http://www.anttieskola.com,http://anttieskola.azuwebsites.net,http://localhost:65432", "*", "*");
            config.EnableCors(cors);

            // Web API configuration and services
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}"
            );
        }
    }
}
