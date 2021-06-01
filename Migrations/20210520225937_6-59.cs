using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class _659 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIdentifier",
                table: "Product",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_UserIdentifier",
                table: "Product",
                column: "UserIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_UserIdentifier",
                table: "Product",
                column: "UserIdentifier",
                principalTable: "User",
                principalColumn: "UserIdentifier",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_UserIdentifier",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_UserIdentifier",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UserIdentifier",
                table: "Product");
        }
    }
}
