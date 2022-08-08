using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineResturnatManagement.Server.Migrations
{
    public partial class SoftwareSetingsMigrationUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrinterId",
                table: "SoftwareSettings");

            migrationBuilder.AddColumn<int>(
                name: "SoftwareSettingsId",
                table: "Printers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Printers_SoftwareSettingsId",
                table: "Printers",
                column: "SoftwareSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_SoftwareSettings_SoftwareSettingsId",
                table: "Printers",
                column: "SoftwareSettingsId",
                principalTable: "SoftwareSettings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Printers_SoftwareSettings_SoftwareSettingsId",
                table: "Printers");

            migrationBuilder.DropIndex(
                name: "IX_Printers_SoftwareSettingsId",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "SoftwareSettingsId",
                table: "Printers");

            migrationBuilder.AddColumn<int>(
                name: "PrinterId",
                table: "SoftwareSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
