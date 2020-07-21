using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class AddNativeIdtoCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DealerCode",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "NativeId",
                table: "Cars",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NativeId",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "DealerCode",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
