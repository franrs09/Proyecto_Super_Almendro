// Models/CategoriaProducto.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperAlmendros.Models
{
    public class CategoriaProducto
    {
        [Key]
        public int idCategoria { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        // Navegación a Productos
        public ICollection<Producto> Productos { get; set; }
    }
}

