using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen3.Data.Migrations
{
    public partial class AgregaBebida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bebidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoPlatillo = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bebidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bebidas_TipoPlatillos_IdTipoPlatillo",
                        column: x => x.IdTipoPlatillo,
                        principalTable: "TipoPlatillos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoPlatillo = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entradas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entradas_TipoPlatillos_IdTipoPlatillo",
                        column: x => x.IdTipoPlatillo,
                        principalTable: "TipoPlatillos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sandwiches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoPlatillo = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sandwiches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sandwiches_TipoPlatillos_IdTipoPlatillo",
                        column: x => x.IdTipoPlatillo,
                        principalTable: "TipoPlatillos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bebidas_IdTipoPlatillo",
                table: "Bebidas",
                column: "IdTipoPlatillo");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_IdTipoPlatillo",
                table: "Entradas",
                column: "IdTipoPlatillo");

            migrationBuilder.CreateIndex(
                name: "IX_Sandwiches_IdTipoPlatillo",
                table: "Sandwiches",
                column: "IdTipoPlatillo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bebidas");

            migrationBuilder.DropTable(
                name: "Entradas");

            migrationBuilder.DropTable(
                name: "Sandwiches");
        }
    }
}
