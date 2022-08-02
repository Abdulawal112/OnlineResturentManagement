using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineResturnatManagement.Server.Migrations
{
    public partial class AddCreateByAndTimeInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavigationMenu_NavigationMenu_ParentMenuId",
                table: "NavigationMenu");

            migrationBuilder.DropIndex(
                name: "IX_NavigationMenu_ParentMenuId",
                table: "NavigationMenu");

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateBy",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_NavigationMenu_ParentMenuId",
                table: "NavigationMenu",
                column: "ParentMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_NavigationMenu_NavigationMenu_ParentMenuId",
                table: "NavigationMenu",
                column: "ParentMenuId",
                principalTable: "NavigationMenu",
                principalColumn: "Id");
        }
    }
}
