using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Super_Almendro.Models
{
    public class MetodoPago
    {
        [Key]
        public int idMetodoPago { get; set; }
        public string Nombre { get; set; }
    }
}