using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Proyecto_Super_Almendro.Models;
using SuperAlmendros.Models;
using System.Data.Entity;

namespace Proyecto_Super_Almendro.Controllers
{
    public class ResenasController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Resenas
        public ActionResult Index()
        {
            var reseñas = db.Resenas.Include(r => r.Producto).Include(r => r.Usuario).ToList();
            return View(reseñas);
        }

        // GET: Resenas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resena = db.Resenas.Find(id);
            if (resena == null)
            {
                return HttpNotFound();
            }
            return View(resena);
        }

        // GET: Resenas/Create
        public ActionResult Create()
        {
            ViewBag.idProducto = new SelectList(db.Productos, "ID_Producto", "Nombre");
            ViewBag.idUsuario = new SelectList(db.Usuarios, "ID_Usuario", "Nombre");
            return View();
        }

        // POST: Resenas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idResena,idProducto,idUsuario,Calificacion,Comentario,Fecha")] Resena resena)
        {
            if (ModelState.IsValid)
            {
                db.Resenas.Add(resena);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idProducto = new SelectList(db.Productos, "ID_Producto", "Nombre", resena.idProducto);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "ID_Usuario", "Nombre", resena.idUsuario);
            return View(resena);
        }

        // GET: Resenas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resena = db.Resenas.Find(id);
            if (resena == null)
            {
                return HttpNotFound();
            }
            ViewBag.idProducto = new SelectList(db.Productos, "ID_Producto", "Nombre", resena.idProducto);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "ID_Usuario", "Nombre", resena.idUsuario);
            return View(resena);
        }

        // POST: Resenas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idResena,idProducto,idUsuario,Calificacion,Comentario,Fecha")] Resena resena)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resena).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idProducto = new SelectList(db.Productos, "ID_Producto", "Nombre", resena.idProducto);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "ID_Usuario", "Nombre", resena.idUsuario);
            return View(resena);
        }

        // GET: Resenas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resena = db.Resenas.Find(id);
            if (resena == null)
            {
                return HttpNotFound();
            }
            return View(resena);
        }

        // POST: Resenas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var resena = db.Resenas.Find(id);
            db.Resenas.Remove(resena);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}