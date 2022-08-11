using Microsoft.EntityFrameworkCore;
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

        public async Task<List<CreditCard>> GetCreditCards()
        {
            return await _context.CreditCards.ToListAsync();    
        }

        public async Task<List<CustomerSetup>> GetCustomersInfo()
        {
            return await _context.CustomerSetups.ToListAsync();
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
            throw new NotImplementedException();
        }

        public Task<UnitOfMeasure> UpdateUnit(UnitOfMeasure uom)
        {
            throw new NotImplementedException();
        }
    }
}
