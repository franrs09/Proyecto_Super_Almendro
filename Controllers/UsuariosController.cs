using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_Super_Almendro.Models;

namespace Proyecto_Super_Almendro.Controllers
{
    public class UsuariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Usuarios/Registrar
        public ActionResult Registrar()
        {
            return View();
        }

        // POST: Usuarios/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar([Bind(Include = "ID_Usuario,Nombre,Email,Dirección,Teléfono,Contraseña,FechaRegistro")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el email ya existe en la base de datos
                var existingUser = db.Usuarios.FirstOrDefault(u => u.Email == usuario.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "El correo electrónico ya está registrado.");
                    return View(usuario);
                }

                usuario.FechaRegistro = DateTime.Now;
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("IniciarSesion");
            }

            return View(usuario);
        }

        // GET: Usuarios/IniciarSesion
        public ActionResult IniciarSesion()
        {
            return View();
        }

        // POST: Usuarios/IniciarSesion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IniciarSesion(string email, string contraseña)
        {
            if (ModelState.IsValid)
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.Email == email && u.Contraseña == contraseña);

                if (usuario != null)
                {
                    // guarda el usuario autenticado en la sesión
                    Session["UsuarioID"] = usuario.ID_Usuario;
                    Session["UsuarioNombre"] = usuario.Nombre;
                    Session["Admin"] = usuario.Admin;
                    if (usuario.Admin)
                    {
                        return RedirectToAction("IndexAdmin", "Administrador"); 
                    }

                    return RedirectToAction("Perfil");
                }

                // contraseña incorrecta
                ModelState.AddModelError("", "Email o contraseña incorrectos.");
            }

            return View();
        }

        public ActionResult Perfil()
        {
            if (Session["UsuarioID"] == null)
            {
                return RedirectToAction("IniciarSesion");
            }

            int usuarioID = (int)Session["UsuarioID"];
            var usuario = db.Usuarios.Find(usuarioID);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }


        // GET: Usuarios/EditarPerfil/5
        public ActionResult EditarPerfil(int? id)
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

        // POST: Usuarios/EditarPerfil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPerfil([Bind(Include = "ID_Usuario,Nombre,Email,Dirección,Teléfono, Contraseña,FechaRegistro")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Perfil");
            }
            return View(usuario);
        }
    }
}


