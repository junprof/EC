using System.Web.Http;
using System.Web.Http.Cors;

namespace DTcms.Web
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //启用跨域
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}