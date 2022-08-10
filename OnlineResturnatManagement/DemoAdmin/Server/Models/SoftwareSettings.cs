using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineResturnatManagement.Server.Models
{
    public class SoftwareSettings
    {

        public int Id { get; set; }
        public int? ItemLevel { get; set; }
        [DefaultValue(false)]
        public bool ModifierEnable { get; set; }
        [DefaultValue(false)]
        public bool ItemWiseModifierEnable { get; set; }
        [DefaultValue(false)]
        public bool WithOutStockSaleEnable { get; set; }
        [DefaultValue(false)]
        public bool ItemRecipeEnable { get; set; }
        public int? RawMaterialLevel { get; set; }
        [DefaultValue(false)]
        public bool PreparationModuleEnable { get; set; }
        [DefaultValue(false)]
        public bool PrintKotEnable { get; set; }
        [DefaultValue(false)]
        public bool ManageTableEnable { get; set; }
        public int? NumberOfTable { get; set; }
        [DefaultValue(false)]
        public bool CustomerDisplayEnable { get; set; }
        [DefaultValue(false)]
        public bool IsAndriodEnable { get; set; }
        [DefaultValue(false)]
        public bool IsKotSerialEnable { get; set; }
        [DefaultValue(false)]
        public bool IsSdChargeApplyEnable { get; set; }
        [DefaultValue(false)]
        public bool KotA4PrintEnable { get; set; }
        [DefaultValue(false)]
        public bool IsSdcEnable { get; set; }
        public string SdCode { get; set; } = String.Empty;
        [DefaultValue(false)]
        public bool PriceIncldVatEnable { get; set; }
        [Column(TypeName = ("decimal(18,5)"))]
        public Decimal? DefaultVat { get; set; }
        [Column(TypeName = ("decimal(18,5)"))]
        public Decimal? TakeWayVat { get; set; }
        [DefaultValue(false)]
        public bool ServiceChargeApplicableEnable { get; set; }
        [DefaultValue(false)]
        public bool ServiceChargeInPercantEnable { get; set; }
        [Column(TypeName = ("decimal(18,5)"))]
        public Decimal? ServiceCharge { get; set; }
        [Column(TypeName = ("decimal(18,5)"))]
        public Decimal? ServiceChargeVat { get; set; }
        [DefaultValue(false)]
        public bool KitchenDisplayEnable { get; set; }
        [DefaultValue(false)]
        public bool ServingDisplayEnable { get; set; }
        public int? ServingDisplayInterval { get; set; }
        [DefaultValue(false)]
        public bool MangeWaiterEnable { get; set; }
        [Column(TypeName = ("decimal(18,5)"))]
        public Decimal? NightHour { get; set; }
        [DefaultValue(false)]
        public bool VatAfterDisscountEnable { get; set; }
        [DefaultValue(false)]
        public bool LastDiscountNoteEnable { get; set; }
        [Column(TypeName = ("decimal(18,5)"))]
        public Decimal? MaxDiscount { get; set; }
        [DefaultValue(false)]
        public bool IsOrderQtyChangeEnable { get; set; }
        public string VatCode { get; set; } = String.Empty;
        [DefaultValue(false)]
        public bool PriceIncludingSdEnable { get; set; }
        public int? PrinterId { get; set; }
        //[NotMapped]
        //public List<Printer>? Printers { get; set; }

    }
}
