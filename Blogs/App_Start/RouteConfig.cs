using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blogs
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "About-Me",
               url: "gioi-thieu-blog",
               defaults: new { controller = "Home", action = "About" }
            );

            routes.MapRoute(
                name: "Category",
                url: "danh-muc",
                defaults: new { controller = "Category", action = "Index" }
            );

            routes.MapRoute(
                name: "CategoryDetail",
                url: "danh-muc/{alias}-{Id}",
                defaults: new { controller = "Category", action = "Detail", Id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ContentDetail",
                url: "{CategoryAlias}-{CategoryId}/{alias}-{Id}",
                defaults: new { controller = "Category", action = "ContentDetail", Id = UrlParameter.Optional }
            );
            routes.MapRoute(
            "Sitemap",
            "sitemap.xml",
            new { controller = "Home", action = "SitemapXml" }
            );

            //routes.MapRoute("RobotsText", "robots.txt", new { controller = "Home", action = "RobotsText" });
            routes.MapRoute(name: "RobotsText", url: "robots.txt", defaults: new { controller = "Home", action = "RobotsText" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("robots.txt");
        }
    }
}
