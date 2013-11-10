﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FurnitureShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(null,
            "",
            new
            {
                controller = "Products",
                action = "List",
                category = (string)null,
                page = 1
            }
            );
            routes.MapRoute(null,
            "Page{page}",
            new { controller = "Products", action = "List", category = (string)null },
            new { page = @"\d+" }
            );
            routes.MapRoute(null,
            "{category}",
            new { controller = "Products", action = "List", page = 1 }
            );
            routes.MapRoute(null,
            "{category}/Page{page}",
            new { controller = "Products", action = "List" },
            new { page = @"\d+" }
            );
            routes.MapRoute(null, "{controller}/{action}");

            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Shop",
            //    url: "Shop",
            //    defaults: new { controller = "Products", action = "List", id = UrlParameter.Optional }
            //);
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: null,
            //    url: "Page{page}",
            //    defaults: new { Controller = "Products", action = "List" }
            //);

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new
            //    {
            //        controller = "Products",
            //        action = "List",
            //        id = UrlParameter.Optional
            //    }
            //);

        }
    }
}