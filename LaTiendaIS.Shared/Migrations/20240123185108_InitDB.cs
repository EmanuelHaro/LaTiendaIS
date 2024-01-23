using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaTiendaIS.Shared.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    DescripcionCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "ColorArticulo",
                columns: table => new
                {
                    IdColor = table.Column<int>(type: "int", nullable: false),
                    DescripcionColor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorArticulo", x => x.IdColor);
                });

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    IdMarca = table.Column<int>(type: "int", nullable: false),
                    DescripcionMarca = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.IdMarca);
                });

            migrationBuilder.CreateTable(
                name: "Talle",
                columns: table => new
                {
                    IdTalle = table.Column<int>(type: "int", nullable: false),
                    DescripcionTalle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talle", x => x.IdTalle);
                });

            migrationBuilder.CreateTable(
                name: "Articulo",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Costo = table.Column<double>(type: "float", nullable: false),
                    MargenDeGanacia = table.Column<float>(type: "real", nullable: false),
                    NetoGravado = table.Column<float>(type: "real", nullable: false),
                    IVA = table.Column<float>(type: "real", nullable: false),
                    PrecioDeVenta = table.Column<double>(type: "float", nullable: false),
                    IdMarca = table.Column<int>(type: "int", nullable: false),
                    IdTalle = table.Column<int>(type: "int", nullable: false),
                    IdColor = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulo", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Articulo_Categoria_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articulo_ColorArticulo_IdColor",
                        column: x => x.IdColor,
                        principalTable: "ColorArticulo",
                        principalColumn: "IdColor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articulo_Marca_IdMarca",
                        column: x => x.IdMarca,
                        principalTable: "Marca",
                        principalColumn: "IdMarca",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articulo_Talle_IdTalle",
                        column: x => x.IdTalle,
                        principalTable: "Talle",
                        principalColumn: "IdTalle",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_IdCategoria",
                table: "Articulo",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_IdColor",
                table: "Articulo",
                column: "IdColor");

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_IdMarca",
                table: "Articulo",
                column: "IdMarca");

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_IdTalle",
                table: "Articulo",
                column: "IdTalle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articulo");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "ColorArticulo");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "Talle");
        }
    }
}
