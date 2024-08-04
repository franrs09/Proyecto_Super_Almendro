using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Super_Almendro.Models
{
    public class Pago
    {
        [Key]
        public int idPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Metodo { get; set; }
        public int idPedido { get; set; }
    }
}