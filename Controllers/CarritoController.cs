using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Proyecto_Super_Almendro.Models;
using SuperAlmendros.Models;

namespace Proyecto_Super_Almendro.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // Acción para agregar un producto al carrito
        [HttpPost]
        public ActionResult AddToCart(int idProducto, int cantidad)
        {
            // Obtener el nombre de usuario desde la sesión
            var userName = Session["UsuarioNombre"]?.ToString(); // Asegúrate de usar la clave correcta

            if (userName == null)
            {
                // Si el usuario no está autenticado, redirigir a la página de inicio de sesión
                return RedirectToAction("IniciarSesion", "Usuarios");
            }

            var usuario = db.Usuarios.FirstOrDefault(u => u.Nombre == userName);

            if (usuario == null)
            {
                return RedirectToAction("Registrar", "Usuarios");
            }

            var carrito = db.Carritos.FirstOrDefault(c => c.idUsuario == usuario.ID_Usuario);

            if (carrito == null)
            {
                carrito = new Carrito
                {
                    idUsuario = usuario.ID_Usuario
                };
                db.Carritos.Add(carrito);
                db.SaveChanges();
            }

            var productoCarrito = db.ProductosCarrito
                .FirstOrDefault(pc => pc.idCarrito == carrito.idCarrito && pc.idProducto == idProducto);

            if (productoCarrito != null)
            {
                productoCarrito.Cantidad += cantidad;
            }
            else
            {
                productoCarrito = new ProductoCarrito
                {
                    idCarrito = carrito.idCarrito,
                    idProducto = idProducto,
                    Cantidad = cantidad
                };
                db.ProductosCarrito.Add(productoCarrito);
            }

            db.SaveChanges();

            return RedirectToAction("Index", "Carrito");
        }

        // Acción para ver el carrito
        public ActionResult Index()
        {
            var userName = Session["UsuarioNombre"]?.ToString(); // Asegúrate de usar la clave correcta
            if (userName == null)
            {
                return RedirectToAction("IniciarSesion", "Usuarios");
            }

            var usuario = db.Usuarios.FirstOrDefault(u => u.Nombre == userName);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var carrito = db.Carritos
                .Include("ProductosCarrito.Producto")
                .FirstOrDefault(c => c.idUsuario == usuario.ID_Usuario);

            if (carrito == null)
            {
                carrito = new Carrito { ProductosCarrito = new List<ProductoCarrito>() };
            }

            return View(carrito);
        }

        // Acción para eliminar un producto del carrito
        [HttpPost]
        public ActionResult RemoveFromCart(int idProductoCarrito)
        {
            var productoCarrito = db.ProductosCarrito.Find(idProductoCarrito);

            if (productoCarrito != null)
            {
                db.ProductosCarrito.Remove(productoCarrito);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
