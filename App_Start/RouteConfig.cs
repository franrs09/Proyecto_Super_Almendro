using System.Web.Mvc;
using System.Web.Routing;

namespace Proyecto_Super_Almendro
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Ruta específica para la página de pago
            routes.MapRoute(
                name: "Payment",
                url: "Carrito/Pago/{id}",
                defaults: new { controller = "Carrito", action = "Pago", id = UrlParameter.Optional }
            );

            // Ruta predeterminada
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}