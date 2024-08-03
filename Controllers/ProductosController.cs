using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SuperAlmendros.Models;
using Proyecto_Super_Almendro.Models;

namespace SuperAlmendros.Controllers
{
    public class ProductosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Productos
        public ActionResult Index()
        {
            var productos = db.Productos.Include(p => p.Categoria).Include(p => p.Imagenes).ToList();
            return View(productos);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "Nombre");
            ViewBag.Estados = new SelectList(new List<string> { "Activo", "Inactivo" });
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProducto,CodigoProducto,Nombre,Descripcion,Precio,Stock,idCategoria,Estado")] Producto producto, IEnumerable<HttpPostedFileBase> imagenes)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Productos.Add(producto);
                    db.SaveChanges();

                    GuardarImagenes(producto.idProducto, imagenes);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the error
                    // LogError(ex);

                    // Add a generic error message
                    ModelState.AddModelError("", "Ocurrió un error al guardar el producto. Por favor, inténtelo de nuevo.");
                }
            }

            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "Nombre", producto.idCategoria);
            ViewBag.Estados = new SelectList(new List<string> { "Activo", "Inactivo" }, producto.Estado);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Producto producto = db.Productos.Include(p => p.Imagenes).FirstOrDefault(p => p.idProducto == id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "Nombre", producto.idCategoria);
            ViewBag.Estados = new SelectList(new List<string> { "Activo", "Inactivo" }, producto.Estado);

            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProducto,CodigoProducto,Nombre,Descripcion,Precio,Stock,idCategoria,Estado")] Producto producto, IEnumerable<HttpPostedFileBase> imagenes)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();

                    // Guardar nuevas imágenes asociadas al producto
                    GuardarImagenes(producto.idProducto, imagenes);

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Producto)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();

                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("", "El producto ya no existe.");
                    }
                    else
                    {
                        var databaseValues = (Producto)databaseEntry.ToObject();

                        if (databaseValues.Nombre != clientValues.Nombre)
                        {
                            ModelState.AddModelError("Nombre", "El nombre del producto ha cambiado.");
                        }

                        clientValues.RowVersion = databaseValues.RowVersion;
                        db.Entry(clientValues).OriginalValues["RowVersion"] = databaseValues.RowVersion;
                    }
                }
                catch (Exception ex)
                {
                    // Registrar el error
                    // Trace.WriteLine($"Error al actualizar el producto: {ex.Message}");

                    // Mensaje de error genérico
                    ModelState.AddModelError("", "Ocurrió un error al actualizar el producto. Por favor, inténtelo de nuevo.");
                }
            }

            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "Nombre", producto.idCategoria);
            ViewBag.Estados = new SelectList(new List<string> { "Activo", "Inactivo" }, producto.Estado);

            return View(producto);
        }

        // Método para guardar la imagen y retornar la URL
        private string SaveImage(HttpPostedFileBase imagen)
        {
            var fileName = Path.GetFileName(imagen.FileName);
            var path = Path.Combine(Server.MapPath("~/wwwroot/Images/Productos"), fileName);

            // Verifica si la ruta de destino existe, de lo contrario, créala
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            imagen.SaveAs(path);
            return Url.Content(Path.Combine("~/wwwroot/Images/Productos", fileName));
        }

        // Método para guardar las imágenes asociadas al producto
        private void GuardarImagenes(int idProducto, IEnumerable<HttpPostedFileBase> imagenes)
        {
            if (imagenes != null)
            {
                foreach (var imagen in imagenes)
                {
                    if (imagen != null && imagen.ContentLength > 0)
                    {
                        var url = SaveImage(imagen);
                        db.ImagenesProductos.Add(new ImagenProducto { idProducto = idProducto, UrlImagen = url });
                    }
                }

                db.SaveChanges();
            }
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Producto producto = db.Productos.Include(p => p.Categoria)
                                            .Include(p => p.Imagenes)
                                            .FirstOrDefault(p => p.idProducto == id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Producto producto = db.Productos.Include(p => p.Imagenes).FirstOrDefault(p => p.idProducto == id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            // Eliminar imágenes asociadas
            var imagenes = db.ImagenesProductos.Where(i => i.idProducto == id).ToList();
            foreach (var imagen in imagenes)
            {
                var path = Server.MapPath(imagen.UrlImagen);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                db.ImagenesProductos.Remove(imagen);
            }

            db.Productos.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

