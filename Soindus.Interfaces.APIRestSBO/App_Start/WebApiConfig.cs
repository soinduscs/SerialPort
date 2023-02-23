using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using Soindus.Interfaces.Modelos.SOcomData;

namespace Soindus.Interfaces.APIRestSBO
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "SoindusApi/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<RegCom>("RegComOdata");
            config.Routes.MapODataServiceRoute("SOcomData", "SOcomData", builder.GetEdmModel());
        }
    }
}
