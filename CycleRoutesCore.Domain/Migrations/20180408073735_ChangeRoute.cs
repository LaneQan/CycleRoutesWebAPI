using Microsoft.EntityFrameworkCore.Migrations;

namespace CycleRoutesCore.Domain.Migrations
{
    public partial class ChangeRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LineType",
                table: "Routes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Routes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineType",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Routes");
        }
    }
}