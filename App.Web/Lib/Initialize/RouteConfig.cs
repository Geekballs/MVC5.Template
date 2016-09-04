using System.Web.Mvc;
using System.Web.Routing;

namespace App.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routeCollection)
        {
            routeCollection.LowercaseUrls = true;
            routeCollection.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routeCollection.MapMvcAttributeRoutes();

            routeCollection.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "App",
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }
    }
}
