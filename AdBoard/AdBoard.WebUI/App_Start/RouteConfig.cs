using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdBoard.WebUI
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
                    controller = "Ad",
                    action = "List",
                    category = (string)null,
                    page = 1
                });

            routes.MapRoute(
                null,
                "UserAds{page}",
                new { controller = "User", action = "UserAds" },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                null,
                "UserAds",
                new { controller = "User", action = "UserAds" }
            );

            routes.MapRoute(
                null,
                "EditProfile",
                new { controller = "Account", action = "EditProfile" }
            );

            routes.MapRoute(
                null,
                "Search",
                new { controller = "Ad", action = "SearchAds" }
            );

            routes.MapRoute(
                name: null,
                url: "AdInfo{id}",
                defaults: new { controller = "Ad", action = "AdInfo" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Ad", action = "List", category = (string)null },
                constraints: new { page = @"\d+"}
            );

            routes.MapRoute(
                null,
                "{category}",
                new { controller = "Ad", action = "List", page = 1}
            );

            routes.MapRoute(
                null,
                "{category}/Page{page}",
                new { controller = "Ad", action = "List"},
                new { page = @"\d+"}
            );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
