using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaTiendaIS.Shared.Migrations
{
    /// <inheritdoc />
    public partial class ModificoArticulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IVA",
                table: "Articulo");

            migrationBuilder.DropColumn(
                name: "PrecioDeVenta",
                table: "Articulo");

            migrationBuilder.RenameColumn(
                name: "NetoGravado",
                table: "Articulo",
                newName: "PorcentajeIVA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PorcentajeIVA",
                table: "Articulo",
                newName: "NetoGravado");

            migrationBuilder.AddColumn<float>(
                name: "IVA",
                table: "Articulo",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<double>(
                name: "PrecioDeVenta",
                table: "Articulo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
