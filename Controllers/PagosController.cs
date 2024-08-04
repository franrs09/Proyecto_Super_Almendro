using Proyecto_Super_Almendro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Super_Almendro.Controllers
{
    public class PagosController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult SeleccionarMetodoPago()
        {
            var metodosPago = db.MetodosPago.ToList();
            return View(metodosPago);
        }

    
        [HttpPost]
        public ActionResult ProcesarPago(int idMetodoPago)
        {
            var userName = Session["UsuarioNombre"]?.ToString();

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

            if (carrito == null || !carrito.ProductosCarrito.Any())
            {
                return RedirectToAction("Index", "Carrito");
            }

            var montoTotal = carrito.ProductosCarrito.Sum(pc => pc.Producto.Precio * pc.Cantidad);

            var metodoPago = db.MetodosPago.FirstOrDefault(mp => mp.idMetodoPago == idMetodoPago);
            if (metodoPago == null)
            {
                return HttpNotFound();
            }

            var pago = new Pago
            {
                idPedido = carrito.idCarrito, // Asumiendo que idPedido refiere al carrito
                Monto = montoTotal,
                Fecha = DateTime.Now,
                Metodo = metodoPago.Nombre
            };

            db.Pagos.Add(pago);
            db.SaveChanges();

            // Vaciar el carrito después del pago
            db.ProductosCarrito.RemoveRange(carrito.ProductosCarrito);
            db.SaveChanges();

            return RedirectToAction("ConfirmacionPago");
        }

        // Acción para mostrar la confirmación de pago
        public ActionResult ConfirmacionPago()
        {
            return View();
        }
    }
}