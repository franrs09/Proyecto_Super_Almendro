using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAlmendros.Models
{
    [Table("ProductoCarrito")]
    public class ProductoCarrito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProductoCarrito { get; set; }

        [ForeignKey("Carrito")]
        public int idCarrito { get; set; }

        [ForeignKey("Producto")]
        public int idProducto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        public virtual Carrito Carrito { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
