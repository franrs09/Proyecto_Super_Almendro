using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Super_Almendro.Models
{
    public class LoginViewModel
    {
        //para controlar el inicio de sesion
        public string Email { get; set; }
        public string Contraseña { get; set; }
    }
}