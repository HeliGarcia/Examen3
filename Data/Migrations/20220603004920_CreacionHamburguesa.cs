using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen3.Data.Migrations
{
    public partial class CreacionHamburguesa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hamburguesas",
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
                    table.PrimaryKey("PK_Hamburguesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hamburguesas_TipoPlatillos_IdTipoPlatillo",
                        column: x => x.IdTipoPlatillo,
                        principalTable: "TipoPlatillos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hamburguesas_IdTipoPlatillo",
                table: "Hamburguesas",
                column: "IdTipoPlatillo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hamburguesas");
        }
    }
}
