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
