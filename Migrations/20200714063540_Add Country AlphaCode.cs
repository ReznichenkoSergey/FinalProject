using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class AddCountryAlphaCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dealers_Countries_CountryId",
                table: "Dealers");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Dealers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Countries",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Countries",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dealers_Countries_CountryId",
                table: "Dealers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dealers_Countries_CountryId",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Countries");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Dealers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Countries",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dealers_Countries_CountryId",
                table: "Dealers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
