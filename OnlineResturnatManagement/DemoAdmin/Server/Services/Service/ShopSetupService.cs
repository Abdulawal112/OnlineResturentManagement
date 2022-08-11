using OnlineResturnatManagement.Server.Data;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;

namespace OnlineResturnatManagement.Server.Services.Service
{
    public class ShopSetupService : IShopSetupService
    {
        public ApplicationDbContext _context;
        
        public ShopSetupService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CounterInfo> CreateCounter(CounterInfo counter)
        {
            await _context.Counters.AddAsync(counter);
             await _context.SaveChangesAsync();
            return counter;

        }

        public Task<Kitchen> CreateKitchen(Kitchen kitchen)
        {
            throw new NotImplementedException();
        }

        public Task<UnitOfMeasure> CreateUnit(UnitOfMeasure uom)
        {
            throw new NotImplementedException();
        }

        public Task<CounterInfo> GetCounterById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CounterInfo>> GetCounters()
        {
            throw new NotImplementedException();
        }

        public Task<Kitchen> GetKitchenById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Kitchen>> GetKitchens()
        {
            throw new NotImplementedException();
        }

        public Task<UnitOfMeasure> GetUnitById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UnitOfMeasure>> GetUnits()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistCounter(CounterInfo counter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistKitchen(Kitchen kitchen)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistUnit(UnitOfMeasure uom)
        {
            throw new NotImplementedException();
        }

        public Task<CounterInfo> UpdateCounter(CounterInfo counter)
        {
            throw new NotImplementedException();
        }

        public Task<Kitchen> UpdateKitchen(Kitchen kitchen)
        {
            throw new NotImplementedException();
        }

        public Task<UnitOfMeasure> UpdateUnit(UnitOfMeasure uom)
        {
            throw new NotImplementedException();
        }
    }
}
