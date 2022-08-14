using OnlineResturnatManagement.Client.Pages;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Client.Services.IService
{
    public interface IShopHttpService
    {
        public Task<ServiceResponse<List<UnitOfMeasureDto>>> GetAllUOM();
        public Task<ServiceResponse<UnitOfMeasureDto>> CreateUOM(UnitOfMeasureDto uomDto);
        public Task<ServiceResponse<UnitOfMeasureDto>> UpdateUOM(UnitOfMeasureDto uomDto);
        public Task<ServiceResponse<List<KitchenDto>>> GetAllKitchen();
        public Task<ServiceResponse<KitchenDto>> CreateKitchen(KitchenDto kitchenDto);
        public Task<ServiceResponse<KitchenDto>> UpdateKitchen(KitchenDto kitchenDto);
        public Task<ServiceResponse<List<CounterInfoDto>>> GetAllCounter();
        public Task<ServiceResponse<CounterInfoDto>> CreateCounter(CounterInfoDto counterDto);
        public Task<ServiceResponse<CounterInfoDto>> UpdateCounter(CounterInfoDto counterDto);
    }
}
