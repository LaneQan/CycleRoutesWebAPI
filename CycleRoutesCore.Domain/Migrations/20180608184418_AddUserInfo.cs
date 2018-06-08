using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CycleRoutesCore.Domain.Migrations
{
    public partial class AddUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Lastname");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrentCity",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredAt",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrentCity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegisteredAt",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Users",
                newName: "Name");
        }
    }
}
