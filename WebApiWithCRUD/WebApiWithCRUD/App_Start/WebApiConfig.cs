using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using WebApiWithCRUD.Filters;

namespace WebApiWithCRUD
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           

            // to enable attribute routes
            config.MapHttpAttributeRoutes();

            

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new ValidateModelAttribute());
            config.Filters.Add(new ValidateRefHeader());

            

        }
        
    }
}
