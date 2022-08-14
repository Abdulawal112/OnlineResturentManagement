using OnlineResturnatManagement.Server.Models;

namespace OnlineResturnatManagement.Server.Services.IService
{
    public interface IShopSetupService
    {
        //Unit
        public Task<List<UnitOfMeasure>> GetUnits();
        public Task<UnitOfMeasure> GetUnitById(int id);
        public Task<UnitOfMeasure> CreateUnit(UnitOfMeasure uom);
        public Task<UnitOfMeasure> UpdateUnit(UnitOfMeasure uom);
        public Task<bool> IsExistUnit(UnitOfMeasure uom);

        //Kitchen
        public Task<List<Kitchen>> GetKitchens();
        public Task<Kitchen> GetKitchenById(int id);
        public Task<Kitchen> CreateKitchen(Kitchen kitchen);
        public Task<Kitchen> UpdateKitchen(Kitchen kitchen);
        public Task<bool> IsExistKitchen(Kitchen kitchen);

        //CounterInfo
        public Task<List<CounterInfo>> GetCounters();
        public Task<CounterInfo> GetCounterById(int id);
        public Task<CounterInfo> CreateCounter(CounterInfo counter);
        public Task<CounterInfo> UpdateCounter(CounterInfo counter);
        public Task<bool> IsExistCounter(CounterInfo counter);
    }
}
