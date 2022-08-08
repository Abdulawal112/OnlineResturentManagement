using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineResturnatManagement.Server.Migrations
{
    public partial class updateSoftwareSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrinterId",
                table: "SoftwareSettings",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrinterId",
                table: "SoftwareSettings");
        }
    }
}
