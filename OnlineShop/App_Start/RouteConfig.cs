﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Product Category",
                url: "{metatitle}-{cateId}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Product Detail",
                url: "{category}-{cateId}/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "Detail", catId = UrlParameter.Optional, id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Tags",
                url: "tag/{tagId}",
                defaults: new { controller = "Content", action = "Tag", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "gioi-thieu",
                defaults: new { controller = "Content", action = "Detail", id = 2 },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
            name: "Login",
            url: "dang-nhap",
            defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
            namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
            name: "Search",
            url: "tim-kiem",
            defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
            namespaces: new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
            name: "Payment Success",
            url: "hoan-thanh",
            defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
            namespaces: new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "OnlineShop.Controllers" }
            );

        }
    }
}
