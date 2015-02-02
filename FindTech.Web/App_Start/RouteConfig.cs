using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FindTech.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SeoTitle",
                url: "bai-viet/{seoCategoryName}/{seoTitle}",
                defaults: new { controller = "Article", action = "Detail", seoCategoryName = UrlParameter.Optional, seoTitle = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "UnderConstruction", id = UrlParameter.Optional }
            );
        }
    }
}
