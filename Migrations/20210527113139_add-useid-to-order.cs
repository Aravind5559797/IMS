using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class adduseidtoorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIdentifier",
                table: "Order",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserIdentifier",
                table: "Order",
                column: "UserIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UserIdentifier",
                table: "Order",
                column: "UserIdentifier",
                principalTable: "User",
                principalColumn: "UserIdentifier",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_UserIdentifier",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserIdentifier",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UserIdentifier",
                table: "Order");
        }
    }
}
