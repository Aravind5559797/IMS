using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class database638 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Shop_ShopId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Shop_User_UserIdentifier",
                table: "Shop");

            migrationBuilder.DropIndex(
                name: "IX_Product_ShopId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "UserIdentifier",
                table: "Shop",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "ProductShop",
                columns: table => new
                {
                    ProductsProductId = table.Column<int>(type: "int", nullable: false),
                    ShopsShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductShop", x => new { x.ProductsProductId, x.ShopsShopId });
                    table.ForeignKey(
                        name: "FK_ProductShop_Product_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductShop_Shop_ShopsShopId",
                        column: x => x.ShopsShopId,
                        principalTable: "Shop",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductShop_ShopsShopId",
                table: "ProductShop",
                column: "ShopsShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_User_UserIdentifier",
                table: "Shop",
                column: "UserIdentifier",
                principalTable: "User",
                principalColumn: "UserIdentifier",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shop_User_UserIdentifier",
                table: "Shop");

            migrationBuilder.DropTable(
                name: "ProductShop");

            migrationBuilder.AlterColumn<string>(
                name: "UserIdentifier",
                table: "Shop",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ShopId",
                table: "Product",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Shop_ShopId",
                table: "Product",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "ShopId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_User_UserIdentifier",
                table: "Shop",
                column: "UserIdentifier",
                principalTable: "User",
                principalColumn: "UserIdentifier",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
