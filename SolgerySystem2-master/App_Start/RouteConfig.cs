using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SolgerySystem2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
           routes.MapRoute(
               name: "Default1",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Groups", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Default2",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Users", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "Default3",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Payments", action = "Index", id = UrlParameter.Optional }
          );
        }
    }
}
