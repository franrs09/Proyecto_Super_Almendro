using System.Data.Entity;
using SuperAlmendros.Models;  // Asegúrate de que el namespace sea correcto

namespace Proyecto_Super_Almendro.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CategoriaProducto> Categorias { get; set; }
        public DbSet<ProductoCarrito> ProductosCarrito { get; set; }
        public DbSet<ImagenProducto> ImagenesProductos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuración de Producto
            modelBuilder.Entity<Producto>()
                .Property(p => p.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Producto>()
                .HasMany(p => p.Imagenes)
                .WithRequired(i => i.Producto)
                .HasForeignKey(i => i.idProducto)
                .WillCascadeOnDelete(false);

            // Configuración de ImagenProducto
            modelBuilder.Entity<ImagenProducto>()
                .HasKey(i => i.idImagen);

            modelBuilder.Entity<ImagenProducto>()
                .ToTable("ImagenProducto");

            // Configuración de CategoriaProducto
            modelBuilder.Entity<CategoriaProducto>()
                .ToTable("CategoriaProducto");

            // Configuración de Producto
            modelBuilder.Entity<Producto>()
                .ToTable("Producto");

            // Configuración de Carrito
            modelBuilder.Entity<Carrito>()
                .ToTable("Carrito");

            // Configuración de ProductoCarrito
            modelBuilder.Entity<ProductoCarrito>()
                .ToTable("ProductoCarrito");

            base.OnModelCreating(modelBuilder);
        }
    }
}
