// Models/Producto.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAlmendros.Models
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int idProducto { get; set; }

        [Required]
        [StringLength(50)]
        public string CodigoProducto { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }

        public int? idCategoria { get; set; }
        [ForeignKey("idCategoria")]
        public CategoriaProducto Categoria { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }


        // Navegación a ProductosCarrito

        public ICollection<ProductoCarrito> ProductosCarrito { get; set; }
        public virtual ICollection<ImagenProducto> Imagenes { get; set; }
    }
}
