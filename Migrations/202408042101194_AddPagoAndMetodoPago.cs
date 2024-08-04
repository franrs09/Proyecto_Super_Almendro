namespace Proyecto_Super_Almendro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPagoAndMetodoPago : DbMigration
    {
        public override void Up()
        {
          
            
            CreateTable(
                "dbo.ProductoCarrito",
                c => new
                    {
                        idProductoCarrito = c.Int(nullable: false, identity: true),
                        idCarrito = c.Int(nullable: false),
                        idProducto = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idProductoCarrito)
                .ForeignKey("dbo.Carrito", t => t.idCarrito, cascadeDelete: true)
                .ForeignKey("dbo.Producto", t => t.idProducto, cascadeDelete: true)
                .Index(t => t.idCarrito)
                .Index(t => t.idProducto);
            
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        idProducto = c.Int(nullable: false, identity: true),
                        CodigoProducto = c.String(nullable: false, maxLength: 50),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        idCategoria = c.Int(),
                        Estado = c.String(nullable: false, maxLength: 20),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.idProducto)
                .ForeignKey("dbo.CategoriaProducto", t => t.idCategoria)
                .Index(t => t.idCategoria);
            
            CreateTable(
                "dbo.CategoriaProducto",
                c => new
                    {
                        idCategoria = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.idCategoria);
            
            CreateTable(
                "dbo.ImagenProducto",
                c => new
                    {
                        idImagen = c.Int(nullable: false, identity: true),
                        idProducto = c.Int(nullable: false),
                        UrlImagen = c.String(),
                    })
                .PrimaryKey(t => t.idImagen)
                .ForeignKey("dbo.Producto", t => t.idProducto)
                .Index(t => t.idProducto);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        ID_Usuario = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Dirección = c.String(maxLength: 250),
                        Teléfono = c.String(maxLength: 15),
                        Contraseña = c.String(nullable: false, maxLength: 100),
                        FechaRegistro = c.DateTime(nullable: false),
                        Admin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Usuario)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.MetodoPago",
                c => new
                    {
                        idMetodoPago = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.idMetodoPago);
            
            CreateTable(
                "dbo.Pago",
                c => new
                    {
                        idPago = c.Int(nullable: false, identity: true),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fecha = c.DateTime(nullable: false),
                        Metodo = c.String(),
                        idPedido = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idPago);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carrito", "idUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.ProductoCarrito", "idProducto", "dbo.Producto");
            DropForeignKey("dbo.ImagenProducto", "idProducto", "dbo.Producto");
            DropForeignKey("dbo.Producto", "idCategoria", "dbo.CategoriaProducto");
            DropForeignKey("dbo.ProductoCarrito", "idCarrito", "dbo.Carrito");
            DropIndex("dbo.Usuarios", new[] { "Email" });
            DropIndex("dbo.ImagenProducto", new[] { "idProducto" });
            DropIndex("dbo.Producto", new[] { "idCategoria" });
            DropIndex("dbo.ProductoCarrito", new[] { "idProducto" });
            DropIndex("dbo.ProductoCarrito", new[] { "idCarrito" });
            DropIndex("dbo.Carrito", new[] { "idUsuario" });
            DropTable("dbo.Pago");
            DropTable("dbo.MetodoPago");
            DropTable("dbo.Usuarios");
            DropTable("dbo.ImagenProducto");
            DropTable("dbo.CategoriaProducto");
            DropTable("dbo.Producto");
            DropTable("dbo.ProductoCarrito");
        }
    }
}
