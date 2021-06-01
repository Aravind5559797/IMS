using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class actulyaddiapitoprodt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApi",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApi",
                table: "Product");
        }
    }
}
