using System.Data.Entity.Migrations;

public partial class AddRowVersionToProducto : DbMigration
{
    public override void Up()
    {
        // Agrega la columna RowVersion a la tabla Producto
        AddColumn("dbo.Producto", "RowVersion", c => c.Binary(nullable: false, fixedLength: true));
        // Define RowVersion como un campo de control de concurrencia optimista
        Sql("UPDATE dbo.Producto SET RowVersion = 0x00"); // Inicializa el valor para las filas existentes
    }

    public override void Down()
    {
        // Elimina la columna RowVersion de la tabla Producto si es necesario revertir la migración
        DropColumn("dbo.Producto", "RowVersion");
    }
}
