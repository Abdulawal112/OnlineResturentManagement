using AutoMapper;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineResturnatManagement.Server.Helper;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Server.Services.Service;
using OnlineResturnatManagement.Shared.DTO;
using System.IO;

namespace OnlineResturnatManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        public ISettingSrevice _settingSrevice;
        private IMapper _mapper;
        private static IWebHostEnvironment _webHostEnvironment;
        private readonly ICashHelper _cashHelper;

        public SettingsController(ISettingSrevice settingSrevice,IMapper mapper, IWebHostEnvironment webHostEnvironment, ICashHelper cashHelper)
        {
            _settingSrevice = settingSrevice;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _cashHelper = cashHelper;
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
        public async Task<ActionResult<CompanyProfileDto>>SaveCompanyProfile([FromBody] CompanyProfileDto profileDto)
        {
            var model = _mapper.Map<CompanyProfile>(profileDto);
            var response = await _settingSrevice.SaveCompanyProfile(model);
            if(response != null)
            {
               
               await SaveImageAsync(model.Id, profileDto);
                _cashHelper.RemoveData(CacheName.CacheCompanyInfo);
                return Ok(response);
            }
            return BadRequest();
        }

        private async Task SaveImageAsync(int id, CompanyProfileDto profileDto)
        {
            string fileExtenstion = "png";// profileDto.File.FileType.ToLower().Contains("png") ? "png" : "jpg";
            string path = _webHostEnvironment.WebRootPath + "\\Images\\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string fileName = "CompanyLogo" + id +"."+ fileExtenstion;
            if (System.IO.File.Exists(path + fileName))
            {
                System.IO.File.Delete(path + fileName);
            }
            using (var fileStream = System.IO.File.Create(path + fileName))
            {
                await fileStream.WriteAsync(profileDto.File.Data);
            }
        }

        [HttpGet("companyProfile")]
        public async Task<ActionResult>GetCompanyProfile()
        {
            var cacheData = _cashHelper.GetData<CompanyProfileDto>(CacheName.CacheCompanyInfo);
            if (cacheData != null)
            {
                return Ok(cacheData);
            }
            var response = await _settingSrevice.GetCompanyProfile();
            if (response != null)
            {
                var modelDto = _mapper.Map<CompanyProfileDto>(response);

                modelDto.File = new FileData();
                string fileName = "CompanyLogo" + response.Id + ".png";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
                if (System.IO.File.Exists(path))
                {
                   modelDto.File.Data = System.IO.File.ReadAllBytes(path);
                }
                else
                {
                    modelDto.File.Data = null;
                }
                _cashHelper.SetData<CompanyProfileDto>(CacheName.CacheCompanyInfo, modelDto);
                return Ok(modelDto);
            }
            return BadRequest();

        }

        [HttpGet("softwareSettings")]
        public async Task<ActionResult> GetSoftwareSettingsConfig()
        {
            SoftwareSettings response = await _settingSrevice.GetSoftwareSettingsConfig();
            var model = _mapper.Map<SoftwareSettingsDto>(response);
            var printers = await _settingSrevice.GetPrinters();
            model.PrinterDtos = _mapper.Map<List<PrinterDto>>(printers);
            return Ok(model);
        }

        [HttpPut("softwareSettings")]
        public async Task<ActionResult<SoftwareSettingsDto>> UpdateSoftSettingsConfig(SoftwareSettingsDto requestSettings)
        {
            var model = _mapper.Map<SoftwareSettings>(requestSettings);
            var response = await _settingSrevice.UpdateSoftwareConfig(model);
            return Ok(_mapper.Map<SoftwareSettingsDto>(response));
        }

    }
}
