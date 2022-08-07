using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Server.Services.Service;

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
    }
}
