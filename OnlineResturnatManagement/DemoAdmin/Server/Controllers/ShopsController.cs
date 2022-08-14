using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OnlineResturnatManagement.Server.Data;
using OnlineResturnatManagement.Server.Helper;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        public IShopSetupService _shopsService;
        private IMapper _mapper;
        private readonly ICashHelper _cashHelper;

        public ShopsController(IShopSetupService shopsService, IMapper mapper, ICashHelper cashHelper)
        {
            _shopsService = shopsService;
            _mapper = mapper;
            _cashHelper = cashHelper;
        }
        [HttpGet("GetUnitOfMeasures")]
        public async Task<ActionResult> GetUnitOfMeasures()
{
            var response = await _shopsService.GetUnits();
            if (response != null)
            {
                return Ok(response);
            }
            return StatusCode(400);
        }
        [HttpPost("CreateUOM")]
        public async Task<ActionResult> CreateUOM(UnitOfMeasureDto unitOfMeasureDto)
        {
            var model = _mapper.Map<UnitOfMeasure>(unitOfMeasureDto);
            if(await _shopsService.IsExistUnit(model))
            {
                return Conflict();
            }
            var response = await _shopsService.CreateUnit(model);
            if (response != null)
            {
                return Created("created", response);
            }
            return StatusCode(400);
        }
        [HttpPut("UpdateUOM")]
        public async Task<ActionResult> UpdateUOM(UnitOfMeasureDto unitOfMeasureDto)
        {
            var model = _mapper.Map<UnitOfMeasure>(unitOfMeasureDto);
            if (await _shopsService.IsExistUnit(model))
            {
                return Conflict();
            }
            var response = await _shopsService.UpdateUnit(model);
            if (response != null)
            {
                return Ok(response);
            }
            return StatusCode(400);
        }
        [HttpGet("GetKitchens")]
        public async Task<ActionResult> GetKitchens()
        {
            var response = await _shopsService.GetKitchens();
            if (response != null)
            {
                return Ok(response);
            }
            return StatusCode(400);
        }
        [HttpPost("CreateKitchen")]
        public async Task<ActionResult> CreateKitchen(KitchenDto kitchenDto)
        {
            var model = _mapper.Map<Kitchen>(kitchenDto);
            if (await _shopsService.IsExistKitchen(model))
            {
                return Conflict();
            }
            var response = await _shopsService.CreateKitchen(model);
            if (response != null)
            {
                return Created("created", response);
            }
            return StatusCode(400);
        }
        [HttpPut("UpdateKitchen")]
        public async Task<ActionResult> UpdateKitchen(KitchenDto kitchenDto)
        {
            var model = _mapper.Map<Kitchen>(kitchenDto);
            if (await _shopsService.IsExistKitchen(model))
            {
                return Conflict();
            }
            var response = await _shopsService.UpdateKitchen(model);
            if (response != null)
            {
                return Ok(response);
            }
            return StatusCode(400);
        }
        [HttpGet("GetCounters")]
        public async Task<ActionResult> GetCounters()
        {
            var response = await _shopsService.GetCounters();
            if (response != null)
            {
                return Ok(response);
            }
            return StatusCode(400);
        }
        [HttpPost("CreateCounter")]
        public async Task<ActionResult> CreateCounter(CounterInfoDto counterInfoDto)
        {
            var model = _mapper.Map<CounterInfo>(counterInfoDto);
            if (await _shopsService.IsExistCounter(model))
            {
                return Conflict();
            }
            var response = await _shopsService.CreateCounter(model);
            if (response != null)
            {
                return Created("created", response);
            }
            return StatusCode(400);
        }
        [HttpPut("UpdateCounter")]
        public async Task<ActionResult> UpdateCounter(CounterInfoDto counterInfoDto)
        {
            var model = _mapper.Map<CounterInfo>(counterInfoDto);
            if (await _shopsService.IsExistCounter(model))
            {
                return Conflict();
            }
            var response = await _shopsService.UpdateCounter(model);
            if (response != null)
            {
                return Ok(response);
            }
            return StatusCode(400);
        }

        [HttpGet("customersInfo")]
        public async Task<ActionResult<List<CustomerSetup>>>GetCustomersInfo()
        {
            var response =await _shopsService.GetCustomersInfo();
            if (response == null || response.Count == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("customerInfo")]
        public async Task<ActionResult<CustomerSetup>> GetCUstomerById(int customerId)
        {
            var response = await _shopsService.GetCUstomerById(customerId);
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPut("customersInfo")]
        public async Task<ActionResult<CustomerSetupDtos>>UpdateCustomerInfo(CustomerSetupDtos customerSetupDtos)
        {
            var model = _mapper.Map<CustomerSetup>(customerSetupDtos);
            var response = await _shopsService.UpdateCustomerInfo(model); 
            return Ok(_mapper.Map<CustomerSetupDtos>(response));
        }

        [HttpGet("creditCards")]
        public async Task<ActionResult<List<CreditCard>>>GetCreditCards()
        {
            var response = await _shopsService.GetCreditCards();
            if(response==null || response.Count == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpGet("creditCard")]
        public async Task<ActionResult<CreditCard>>GetCreditCardInfo(int creditCardId)
        {
            var response = await _shopsService.GetCreditCardInfoById(creditCardId);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPut("creditCards")]
        public async Task<ActionResult<CreditCardDtos>>UpdateCreditInfo(CreditCardDtos creditCardDtos)
        {
            var model = _mapper.Map<CreditCard>(creditCardDtos);
            var response = await _shopsService.UpdateCreditInfo(model);
            return Ok(_mapper.Map<CreditCardDtos>(response));
        }
    }
}
