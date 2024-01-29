using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaTiendaIS.Shared.Migrations
{
    /// <inheritdoc />
    public partial class ModificoPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPagoConTarjeta",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "IdPagoEfectivo",
                table: "Pago");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPagoConTarjeta",
                table: "Pago",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdPagoEfectivo",
                table: "Pago",
                type: "int",
                nullable: true);
        }
    }
}
