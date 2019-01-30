using System.Web.Http;
using WebApiWithCRUD.Filters;
using WebApiWithCRUD.Helper;

namespace WebApiWithCRUD
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           

            // to enable attribute routes
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new ValidateModelAttribute());
            config.Filters.Add(new ValidateRefHeader());
            config.Filters.Add(new BasicAuthorization());


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

           

            

        }
        
    }
}
