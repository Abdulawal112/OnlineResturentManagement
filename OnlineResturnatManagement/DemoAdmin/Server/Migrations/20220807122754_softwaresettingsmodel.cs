using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineResturnatManagement.Server.Migrations
{
    public partial class softwaresettingsmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Printer_SoftwareSettings_SoftwareSettingsId",
                table: "Printer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Printer",
                table: "Printer");

            migrationBuilder.DropIndex(
                name: "IX_Printer_SoftwareSettingsId",
                table: "Printer");

            migrationBuilder.DropColumn(
                name: "SoftwareSettingsId",
                table: "Printer");

            migrationBuilder.RenameTable(
                name: "Printer",
                newName: "Printers");

            migrationBuilder.AddColumn<int>(
                name: "PrinterId",
                table: "SoftwareSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "Printers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Printers",
                table: "Printers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Printers",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "PrinterId",
                table: "SoftwareSettings");

            migrationBuilder.DropColumn(
                name: "Default",
                table: "Printers");

            migrationBuilder.RenameTable(
                name: "Printers",
                newName: "Printer");

            migrationBuilder.AddColumn<int>(
                name: "SoftwareSettingsId",
                table: "Printer",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Printer",
                table: "Printer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Printer_SoftwareSettingsId",
                table: "Printer",
                column: "SoftwareSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Printer_SoftwareSettings_SoftwareSettingsId",
                table: "Printer",
                column: "SoftwareSettingsId",
                principalTable: "SoftwareSettings",
                principalColumn: "Id");
        }
    }
}
