using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class ChangedOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Shop_ShopId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ShopId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ShopId",
                table: "Order",
                newName: "HistShopName");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "HistOrder",
                newName: "HistQuantity");

            migrationBuilder.RenameColumn(
                name: "ProductQuantity",
                table: "HistOrder",
                newName: "HistProductId");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "HistOrder",
                newName: "HistShortDescription");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "HistOrder",
                newName: "HistImage");

            migrationBuilder.AddColumn<int>(
                name: "HistShopId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HistDescription",
                table: "HistOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistName",
                table: "HistOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "HistPrice",
                table: "HistOrder",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HistShopId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "HistDescription",
                table: "HistOrder");

            migrationBuilder.DropColumn(
                name: "HistName",
                table: "HistOrder");

            migrationBuilder.DropColumn(
                name: "HistPrice",
                table: "HistOrder");

            migrationBuilder.RenameColumn(
                name: "HistShopName",
                table: "Order",
                newName: "ShopId");

            migrationBuilder.RenameColumn(
                name: "HistShortDescription",
                table: "HistOrder",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "HistQuantity",
                table: "HistOrder",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "HistProductId",
                table: "HistOrder",
                newName: "ProductQuantity");

            migrationBuilder.RenameColumn(
                name: "HistImage",
                table: "HistOrder",
                newName: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShopId",
                table: "Order",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Shop_ShopId",
                table: "Order",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "ShopId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
