using AutoMapper;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public SettingsController(ISettingSrevice settingSrevice,IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _settingSrevice = settingSrevice;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
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
                return Ok(response);
            }
            return BadRequest(response);
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
                return Ok(modelDto);
            }
            return BadRequest();

        }

    }
}
