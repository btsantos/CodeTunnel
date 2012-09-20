using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using CodeTunnel.Domain.Interfaces;
using CodeTunnel.Domain.Repositories;
using CodeTunnel.Domain.Entities;
using CodeTunnel.MvcUtilities.Components;

namespace CodeTunnel.UI
{
    /// <summary>
    /// Our application inherits from NinjectHttpApplication so that we can handle the
    /// CreateKernel method and register our bindings.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Register application global filters.
        /// </summary>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// Register application routes.
        /// </summary>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                "Content/{containerName}/{blobName}",
                new { controller = "Content", action = "Index" }
            );

            routes.MapRoute(
                null,
                "Content/{blobName}",
                new { controller = "Content", action = "Index" }
            );

            routes.MapRoute(
                null,
                "SetupStorage",
                new { controller = "Content", action = "SetupStorage" }
            );

            routes.MapRoute(
                null,
                "Chat/{nickname}",
                new { controller = "Chat", action = "Index" }
            );

            routes.MapRoute(
                null, // Route name
                "{controller}", // URL with parameters
                new { controller = "Blog", action = "Index", page = 1 } // Parameter defaults
            );

            routes.MapRoute(
                null, // Route name
                "{controller}/Page{page}", // URL with parameters
                new { controller = "Blog", action = "Index", page = @"\d+" } // Parameter defaults
            );

            routes.MapRoute(
                null, // Route name
                "blog/post/{id}/{postTitle}", // URL with parameters
                new { controller = "Blog", action = "Post", id = UrlParameter.Optional, postTitle = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                null, // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Blog", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        public void Application_Start(Object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.DefaultBinder = new DependencyResolvingModelBinder();
        }
    }
}