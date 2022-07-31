using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineResturnatManagement.Server.Migrations
{
    public partial class usertextPasswordToEncrypPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashKey",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashKey",
                table: "Users");
        }
    }
}
