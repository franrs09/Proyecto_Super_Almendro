using Proyecto_Super_Almendro.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Super_Almendro.Models
{
    public class Resena
    {
        [Key]
        public int idResena { get; set; }

        public int idProducto { get; set; }

        public int idUsuario { get; set; }

        [Range(1, 5)]
        public int Calificacion { get; set; }

        [MaxLength(1000)]
        public string Comentario { get; set; }

        public DateTime Fecha { get; set; }

        [ForeignKey("idProducto")]
        public virtual Producto Producto { get; set; }

        [ForeignKey("idUsuario")]
        public virtual Usuario Usuario { get; set; }
    }
}