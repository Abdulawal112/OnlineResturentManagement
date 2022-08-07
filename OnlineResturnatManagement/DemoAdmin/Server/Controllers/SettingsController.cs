using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Server.Services.Service;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        public ISettingSrevice _settingSrevice;
        public SettingsController(ISettingSrevice settingSrevice)
        {
            _settingSrevice = settingSrevice;
        }

        [HttpGet("GetActiveModules")]
        public async Task<ActionResult> GetAllActiveModule()
{
            var response = await _settingSrevice.GetActiveModules();
            if (response != null)
            {
                return Ok(response);
            }
            return StatusCode(400);
        }

        [HttpPut("companyProfile")]
        public async Task<ActionResult<CompanyProfileDto>>SaveCompanyProfile(CompanyProfile companyProfile)
        {
            var response = await _settingSrevice.SaveCompanyProfile(companyProfile);
            if(response != null)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("companyProfile")]
        public async Task<ActionResult<CompanyProfileDto>>GetCompanyProfile()
        {
            var response = await _settingSrevice.GetCompanyProfile();
            if(response != null)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }

    }
}
