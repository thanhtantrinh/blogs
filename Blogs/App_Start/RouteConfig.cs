﻿using System;
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
               url: "tu-su",
               defaults: new { controller = "Home", action = "About" }
            );

            routes.MapRoute(
                name: "Category",
                url: "{metatitle}-{Id}",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional }                
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
