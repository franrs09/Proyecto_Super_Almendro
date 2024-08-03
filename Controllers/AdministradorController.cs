using Proyecto_Super_Almendro.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Super_Almendro.Controllers
{
    public class AdministradorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Administrador

        private bool EsAdministrador()
        {
            return Session["Admin"] != null && (bool)Session["Admin"];
        }
        public ActionResult IndexAdmin()
        {
            if (Session["Admin"] == null || !(bool)Session["Admin"])
            {
                return RedirectToAction("IniciarSesion", "Usuarios");
            }
            else {
                return View();
            }
                   
        }

        public ActionResult GestionUsuarios()
        {
            if (Session["Admin"] == null || !(bool)Session["Admin"])
            {
                return RedirectToAction("IniciarSesion", "Usuarios");
            }
            else
            {
                var usuarios = db.Usuarios.ToList();
                return View(usuarios);
            }

        }
        // GET: Administrador/EliminarUsuario
        public ActionResult EliminarUsuario(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // POST: Administrador/EliminarUsuario
        [HttpPost, ActionName("EliminarUsuario")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarUsuarioConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("GestionUsuarios");
        }

        // GET: Administrador/EditarUsuario/5
        public ActionResult EditarUsuario(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // POST: Administrador/EditarUsuario/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarUsuario([Bind(Include = "ID_Usuario,Nombre,Email,Dirección,Teléfono,Contraseña,FechaRegistro")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GestionUsuarios");
            }

            return View(usuario);
        }

    }
}

