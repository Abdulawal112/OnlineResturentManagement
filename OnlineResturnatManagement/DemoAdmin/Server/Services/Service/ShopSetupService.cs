using OnlineResturnatManagement.Server.Data;
using OnlineResturnatManagement.Server.Migrations;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
using CreditCard = OnlineResturnatManagement.Server.Models.CreditCard;
using CustomerSetup = OnlineResturnatManagement.Server.Models.CustomerSetup;

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
            counter.Code = counter.Id.ToString("D4");
            _context.Counters.Attach(counter).Property(x => x.Code).IsModified = true;
            await _context.SaveChangesAsync();
            return counter;

        }

        public async Task<Kitchen> CreateKitchen(Kitchen kitchen)
        {
            await _context.Kitchens.AddAsync(kitchen);
            await _context.SaveChangesAsync();
            kitchen.Code = kitchen.Id.ToString("D4");
            _context.Kitchens.Attach(kitchen).Property(x => x.Code).IsModified = true;
            await _context.SaveChangesAsync();
            return kitchen;
        }

        public async Task<UnitOfMeasure> CreateUnit(UnitOfMeasure uom)
        {
            await _context.UnitOfMeasures.AddAsync(uom);
            await _context.SaveChangesAsync();

            uom.Code = uom.Id.ToString("D4");
            _context.UnitOfMeasures.Attach(uom).Property(x => x.Code).IsModified = true;
            await _context.SaveChangesAsync();

            return uom;
        }

        public async Task<CounterInfo> GetCounterById(int id)
        {
            return await _context.Counters.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CounterInfo>> GetCounters()
        {
            return await _context.Counters.ToListAsync();
        }

        public async Task<CreditCard> GetCreditCardInfoById(int creditCardId)
        {
            return await _context.CreditCards.FindAsync(creditCardId);
        }

        public async Task<List<CreditCard>> GetCreditCards()
        {
            return await _context.CreditCards.ToListAsync();    
        }

        public async Task<CustomerSetup> GetCUstomerById(int customerId)
        {
            return await _context.CustomerSetups.FindAsync(customerId); 
        }

        public async Task<List<CustomerSetup>> GetCustomersInfo()
        {
            return await _context.CustomerSetups.ToListAsync();
        }

        public Task<Kitchen> GetKitchenById(int id)
        {
            return await _context.Kitchens.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Kitchen>> GetKitchens()
        {
            return await _context.Kitchens.ToListAsync();
        }

        public async Task<UnitOfMeasure> GetUnitById(int id)
        {
            return await _context.UnitOfMeasures.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UnitOfMeasure>> GetUnits()
        {
            return await _context.UnitOfMeasures.ToListAsync();
        }

        public async Task<bool> IsExistCounter(CounterInfo counter)
        {
            bool isExist = false;
            if (counter.Id == 0)
            {
                isExist = await _context.Counters.FirstOrDefaultAsync(x => x.Name == counter.Name) != null ? true : false;
            }
            else
            {
                isExist = await _context.Counters.FirstOrDefaultAsync(x => x.Name == counter.Name && x.Id != counter.Id) != null ? true : false;
            }
            return isExist;
        }

        public async Task<bool> IsExistKitchen(Kitchen kitchen)
        {
            bool isExist = false;
            if (kitchen.Id == 0)
            {
                isExist = await _context.Kitchens.FirstOrDefaultAsync(x => x.Name == kitchen.Name) != null ? true : false;
            }
            else
            {
                isExist = await _context.Counters.FirstOrDefaultAsync(x => x.Name == kitchen.Name && x.Id != kitchen.Id) != null ? true : false;
            }
            return isExist;
        }

        public async Task<bool> IsExistUnit(UnitOfMeasure uom)
        {
            bool isExist = false;
            if (uom.Id == 0)
            {
                isExist = await _context.UnitOfMeasures.FirstOrDefaultAsync(x => x.UOM == uom.UOM) != null ? true : false;
            }
            else
            {
                isExist = await _context.UnitOfMeasures.FirstOrDefaultAsync(x => x.UOM == uom.UOM && x.Id != uom.Id) != null ? true : false;
            }
            return isExist;
        }

        public async Task<CounterInfo> UpdateCounter(CounterInfo counter)
        {
            _context.Counters.Update(counter);
            await _context.SaveChangesAsync();
            return counter;
        }

        public async Task<CreditCard> UpdateCreditInfo(CreditCard creditCard)
        {
            if(creditCard.Id != 0)
            {
                if (await _context.CreditCards.AnyAsync(_ => _.Id == creditCard.Id))
                {
                    _context.CreditCards.Update(creditCard);
                    await _context.SaveChangesAsync();
                    return creditCard;
                }
                return null;
            }
            else
            {
                await _context.CreditCards.AddAsync(creditCard);
                await _context.SaveChangesAsync();
                return creditCard;
            }
        }

        public async Task<CustomerSetup> UpdateCustomerInfo(CustomerSetup requestData)
        {
            if (requestData.Id != 0)
            {
                if (await _context.CustomerSetups.AnyAsync(_ => _.Id == requestData.Id))
                {
                    _context.CustomerSetups.Update(requestData);
                    await _context.SaveChangesAsync();
                    return requestData;
                }
                return null;
            }
            else
            {
                await _context.CustomerSetups.AddAsync(requestData);
                await _context.SaveChangesAsync();
                return requestData;
            }
        }

        public Task<Kitchen> UpdateKitchen(Kitchen kitchen)
        {
            _context.Kitchens.Update(kitchen);
            await _context.SaveChangesAsync();
            return kitchen;
        }

        public async Task<UnitOfMeasure> UpdateUnit(UnitOfMeasure uom)
        {
            _context.UnitOfMeasures.Update(uom);
            await _context.SaveChangesAsync();
            return uom;
        }
    }
}
