using System.Data.Entity;
using Proyecto_Super_Almendro.Models;
using SuperAlmendros.Models;

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
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<Resena> Resenas { get; set; }

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

            // Configuración de Pago
            modelBuilder.Entity<Pago>()
                .ToTable("Pago")
                .HasKey(p => p.idPago);

            modelBuilder.Entity<Pago>()
                .Property(p => p.Monto)
                .IsRequired();

            modelBuilder.Entity<Pago>()
                .Property(p => p.Fecha)
                .IsRequired();

            modelBuilder.Entity<Pago>()
                .Property(p => p.Metodo)
                .IsRequired();

            modelBuilder.Entity<Pago>()
                .Property(p => p.idPedido)
                .IsRequired();

            // Configuración de MetodoPago
            modelBuilder.Entity<MetodoPago>()
                .ToTable("MetodoPago")
                .HasKey(m => m.idMetodoPago);

            modelBuilder.Entity<MetodoPago>()
                .Property(m => m.Nombre)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}