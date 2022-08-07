using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineResturnatManagement.Server.Migrations
{
    public partial class SoftwareSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SoftwareSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemLevel = table.Column<int>(type: "int", nullable: true),
                    ModifierEnable = table.Column<bool>(type: "bit", nullable: false),
                    ItemWiseModifierEnable = table.Column<bool>(type: "bit", nullable: false),
                    WithOutStockSaleEnable = table.Column<bool>(type: "bit", nullable: false),
                    ItemRecipeEnable = table.Column<bool>(type: "bit", nullable: false),
                    RawMaterialLevel = table.Column<int>(type: "int", nullable: true),
                    PreparationModuleEnable = table.Column<bool>(type: "bit", nullable: false),
                    PrintKotEnable = table.Column<bool>(type: "bit", nullable: false),
                    ManageTableEnable = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfTable = table.Column<int>(type: "int", nullable: true),
                    CustomerDisplayEnable = table.Column<bool>(type: "bit", nullable: false),
                    IsAndriodEnable = table.Column<bool>(type: "bit", nullable: false),
                    IsKotSerialEnable = table.Column<bool>(type: "bit", nullable: false),
                    IsSdChargeApplyEnable = table.Column<bool>(type: "bit", nullable: false),
                    KotA4PrintEnable = table.Column<bool>(type: "bit", nullable: false),
                    IsSdcEnable = table.Column<bool>(type: "bit", nullable: false),
                    SdCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceIncldVatEnable = table.Column<bool>(type: "bit", nullable: false),
                    DefaultVat = table.Column<decimal>(type: "decimal(18,5)", nullable: true),
                    TakeWayVat = table.Column<decimal>(type: "decimal(18,5)", nullable: true),
                    ServiceChargeApplicableEnable = table.Column<bool>(type: "bit", nullable: false),
                    ServiceChargeInPercantEnable = table.Column<bool>(type: "bit", nullable: false),
                    ServiceCharge = table.Column<decimal>(type: "decimal(18,5)", nullable: true),
                    ServiceChargeVat = table.Column<decimal>(type: "decimal(18,5)", nullable: true),
                    KitchenDisplayEnable = table.Column<bool>(type: "bit", nullable: false),
                    ServingDisplayEnable = table.Column<bool>(type: "bit", nullable: false),
                    ServingDisplayInterval = table.Column<int>(type: "int", nullable: true),
                    MangeWaiterEnable = table.Column<bool>(type: "bit", nullable: false),
                    NightHour = table.Column<decimal>(type: "decimal(18,5)", nullable: true),
                    VatAfterDisscountEnable = table.Column<bool>(type: "bit", nullable: false),
                    LastDiscountNoteEnable = table.Column<bool>(type: "bit", nullable: false),
                    MaxDiscount = table.Column<decimal>(type: "decimal(18,5)", nullable: true),
                    IsOrderQtyChangeEnable = table.Column<bool>(type: "bit", nullable: false),
                    VatCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceIncludingSdEnable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Printer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftwareSettingsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Printer_SoftwareSettings_SoftwareSettingsId",
                        column: x => x.SoftwareSettingsId,
                        principalTable: "SoftwareSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Printer_SoftwareSettingsId",
                table: "Printer",
                column: "SoftwareSettingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Printer");

            migrationBuilder.DropTable(
                name: "SoftwareSettings");
        }
    }
}
