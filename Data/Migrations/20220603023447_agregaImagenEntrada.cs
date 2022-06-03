using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen3.Data.Migrations
{
    public partial class agregaImagenEntrada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenSandwich",
                table: "Sandwiches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagenHamburguesa",
                table: "Hamburguesas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagenEntrada",
                table: "Entradas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagenBebida",
                table: "Bebidas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenSandwich",
                table: "Sandwiches");

            migrationBuilder.DropColumn(
                name: "ImagenHamburguesa",
                table: "Hamburguesas");

            migrationBuilder.DropColumn(
                name: "ImagenEntrada",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "ImagenBebida",
                table: "Bebidas");
        }
    }
}
