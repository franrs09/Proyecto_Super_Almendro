using SuperAlmendros.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_Super_Almendro.Models
{
    public class Usuario
    {
        [Key]
        public int ID_Usuario { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)] // añade un índice único
        [EmailAddress(ErrorMessage = "Formato de email no válido")]
        public string Email { get; set; }
        [MaxLength(250)]
        public string Dirección { get; set; }
        [StringLength(15)]
        public string Teléfono { get; set; }
        [Required]
        [StringLength(100)]
        public string Contraseña { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Admin { get; set; }

        // Navegación a Carritos
        public ICollection<Carrito> Carritos { get; set; }
    }
}