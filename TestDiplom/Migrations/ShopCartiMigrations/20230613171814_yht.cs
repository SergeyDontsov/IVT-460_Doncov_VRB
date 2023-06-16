using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestDiplom.Migrations.ShopCartiMigrations
{
    public partial class yht : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_cour_t1",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "t1",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_cour_t1",
                table: "Orders",
                column: "t1",
                principalTable: "cour",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_cour_t1",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "t1",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_cour_t1",
                table: "Orders",
                column: "t1",
                principalTable: "cour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
