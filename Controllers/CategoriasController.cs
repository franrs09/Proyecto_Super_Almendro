using System.Linq;
using System.Net;
using System.Web.Mvc;
using SuperAlmendros.Models;
using System.Data.Entity;
using Proyecto_Super_Almendro.Models;
using System;

namespace SuperAlmendros.Controllers
{
    public class CategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categorias
        public ActionResult Index()
        {
            // Obtiene la lista de categorías y la pasa a la vista
            return View(db.Categorias.ToList());
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCategoria,Nombre")] CategoriaProducto categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Categorias.Add(categoria);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones, registra el error si es necesario
                    ModelState.AddModelError("", "Error al guardar los cambios. Inténtalo de nuevo.");
                }
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoriaProducto categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCategoria,Nombre")] CategoriaProducto categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(categoria).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones, registra el error si es necesario
                    ModelState.AddModelError("", "Error al guardar los cambios. Inténtalo de nuevo.");
                }
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoriaProducto categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                CategoriaProducto categoria = db.Categorias.Find(id);
                if (categoria != null)
                {
                    db.Categorias.Remove(categoria);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, registra el error si es necesario
                ModelState.AddModelError("", "Error al eliminar la categoría. Inténtalo de nuevo.");
            }

            return RedirectToAction("Index");
        }
    }
}
