using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAlmendros.Models
{
    public class ImagenProducto
    {
        [Key]
        public int idImagen { get; set; } // Clave primaria

        public int idProducto { get; set; }
        public string UrlImagen { get; set; }

        [ForeignKey("idProducto")]
        public virtual Producto Producto { get; set; }
    }
}