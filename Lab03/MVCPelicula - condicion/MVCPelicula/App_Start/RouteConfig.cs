using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCPelicula
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
            // este se agrego para trabajar en urls del tipo HelloWorld/Welcome?nombre=Juanito&apellido=2, tengo explicado esto en el txt del laboratorio 01
            routes.MapRoute(
                name: "Hola",
                url: "{controller}/{action}/{nombre}/{id}"
            );
        }
    }
}
