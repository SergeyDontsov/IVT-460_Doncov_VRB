using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestDiplom.Migrations.ShopCartiMigrations
{
    public partial class ht : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "familya",
                table: "cour");

            migrationBuilder.DropColumn(
                name: "imya",
                table: "cour");

            migrationBuilder.DropColumn(
                name: "otchestvo",
                table: "cour");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "familya",
                table: "cour",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "imya",
                table: "cour",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "otchestvo",
                table: "cour",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
