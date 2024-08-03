using Proyecto_Super_Almendro.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAlmendros.Models
{
    [Table("Carrito")]
    public class Carrito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCarrito { get; set; }

        [ForeignKey("Usuario")]
        public int idUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }

        // Navegación a ProductoCarrito
        public virtual ICollection<ProductoCarrito> ProductosCarrito { get; set; }
    }
}
