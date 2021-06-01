using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class database407 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shop_Platform_PlatformId",
                table: "Shop");

            migrationBuilder.AlterColumn<int>(
                name: "PlatformId",
                table: "Shop",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_Platform_PlatformId",
                table: "Shop",
                column: "PlatformId",
                principalTable: "Platform",
                principalColumn: "PlatformId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shop_Platform_PlatformId",
                table: "Shop");

            migrationBuilder.AlterColumn<int>(
                name: "PlatformId",
                table: "Shop",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_Platform_PlatformId",
                table: "Shop",
                column: "PlatformId",
                principalTable: "Platform",
                principalColumn: "PlatformId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
