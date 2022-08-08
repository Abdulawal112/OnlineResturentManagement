using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineResturnatManagement.Shared.DTO
{
    public class SoftwareSettingsDto
    {
        public int Id { get; set; }
        public int? ItemLevel { get; set; }
        public bool ModifierEnable { get; set; } = false;
        public bool ItemWiseModifierEnable { get; set; } = false;
        public bool WithOutStockSaleEnable { get; set; } = false;
        public bool ItemRecipeEnable { get; set; } = false;
        public int? RawMaterialLevel { get; set; }
        public bool PreparationModuleEnable { get; set; } = false;
        public bool PrintKotEnable { get; set; } = false;
        public bool ManageTableEnable { get; set; } = false;
        public int? NumberOfTable { get; set; }
        public bool CustomerDisplayEnable { get; set; } = false;
        public bool IsAndriodEnable { get; set; } = false;
        public bool IsKotSerialEnable { get; set; } = false;
        public bool IsSdChargeApplyEnable { get; set; } = false;
        public bool KotA4PrintEnable { get; set; } = false;
        public bool IsSdcEnable { get; set; } = false;
        public string SdCode { get; set; } = String.Empty;
        public bool PriceIncldVatEnable { get; set; } = false;
        [Column(TypeName = ("decimal(18,5)"))]
        public Decimal? DefaultVat { get; set; }
        [Column(TypeName = ("decimal(18,5)"))]
        public Decimal? TakeWayVat { get; set; }
        public bool ServiceChargeApplicableEnable { get; set; } = false;
        public bool ServiceChargeInPercantEnable { get; set; } = false;
        [Column(TypeName = ("decimal(18,5)"))]
        public Decimal? ServiceCharge { get; set; }
        [Column(TypeName = ("decimal(18,5)"))]
        public Decimal? ServiceChargeVat { get; set; }
        public bool KitchenDisplayEnable { get; set; } = false;
        public bool ServingDisplayEnable { get; set; } = false;
        public int? ServingDisplayInterval { get; set; }
        public bool MangeWaiterEnable { get; set; } = false;
        [JsonIgnore]
        [Column(TypeName = ("decimal(18,5)"))]
        public Decimal? NightHour { get; set; }
        public bool VatAfterDisscountEnable { get; set; } = false;
        public bool LastDiscountNoteEnable { get; set; } = false;
        [Column(TypeName = ("decimal(18,5)"))]
        public Decimal? MaxDiscount { get; set; }
        public bool IsOrderQtyChangeEnable { get; set; } = false;
        public string VatCode { get; set; } = String.Empty;
        public bool PriceIncludingSdEnable { get; set; } = true;
        public int PrinterId { get; set; }
        [NotMapped]
        public List<PrinterDto>? PrinterList { get; set; }
        
    }
}
