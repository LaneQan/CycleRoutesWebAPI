using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CycleRoutesCore.Domain.Migrations
{
    public partial class AddDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteImage_Routes_RouteId",
                table: "RouteImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RouteImage",
                table: "RouteImage");

            migrationBuilder.RenameTable(
                name: "RouteImage",
                newName: "RouteImages");

            migrationBuilder.RenameIndex(
                name: "IX_RouteImage_RouteId",
                table: "RouteImages",
                newName: "IX_RouteImages_RouteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RouteImages",
                table: "RouteImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteImages_Routes_RouteId",
                table: "RouteImages",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteImages_Routes_RouteId",
                table: "RouteImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RouteImages",
                table: "RouteImages");

            migrationBuilder.RenameTable(
                name: "RouteImages",
                newName: "RouteImage");

            migrationBuilder.RenameIndex(
                name: "IX_RouteImages_RouteId",
                table: "RouteImage",
                newName: "IX_RouteImage_RouteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RouteImage",
                table: "RouteImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteImage_Routes_RouteId",
                table: "RouteImage",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
