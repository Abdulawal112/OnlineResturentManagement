using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineResturnatManagement.Server.Migrations
{
    public partial class AddCreateByAndTimeInRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateBy",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Roles");
        }
    }
}
