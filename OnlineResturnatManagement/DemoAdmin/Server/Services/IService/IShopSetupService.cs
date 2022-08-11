using OnlineResturnatManagement.Server.Migrations;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Shared.DTO;
using CreditCard = OnlineResturnatManagement.Server.Models.CreditCard;
using CustomerSetup = OnlineResturnatManagement.Server.Models.CustomerSetup;

namespace OnlineResturnatManagement.Server.Services.IService
{
    public interface IShopSetupService
    {
        //Unit
        public Task<List<UnitOfMeasure>> GetUnits();
        public Task<UnitOfMeasure> GetUnitById(int id);
        public Task<UnitOfMeasure> CreateUnit(UnitOfMeasure role);
        public Task<UnitOfMeasure> UpdateUnit(UnitOfMeasure role);
        public Task<bool> IsExistUnit(UnitOfMeasure role);

        //Kitchen
        public Task<List<Kitchen>> GetKitchens();
        public Task<Kitchen> GetKitchenById(int id);
        public Task<Kitchen> CreateKitchen(Kitchen role);
        public Task<Kitchen> UpdateKitchen(Kitchen role);
        public Task<bool> IsExistKitchen(Kitchen role);

        //CounterInfo
        public Task<List<CounterInfo>> GetCounters();
        public Task<CounterInfo> GetCounterById(int id);
        public Task<CounterInfo> CreateCounter(CounterInfo role);
        public Task<CounterInfo> UpdateCounter(CounterInfo role);
        public Task<bool> IsExistCounter(CounterInfo role);

        //CustomerSetup
        Task<List<CustomerSetup>>GetCustomersInfo();
        Task<CustomerSetup> UpdateCustomerInfo(CustomerSetup requestData);

        //CreditCardsInfo
        Task<List<CreditCard>> GetCreditCards();
        Task<CreditCard> UpdateCreditInfo(CreditCard creditCard);
    }
}
