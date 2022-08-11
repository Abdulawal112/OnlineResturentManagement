using OnlineResturnatManagement.Client.Pages.Setting;
using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
using System.Net.Http.Json;
using System.Text.Json;

namespace OnlineResturnatManagement.Client.Services.Service
{
    public class SettingsHttpService : ISettingsHttpService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _options;
        //public List<RoleDto> Roles { get; set; } = new List<RoleDto>();


        public SettingsHttpService(HttpClient http)
        {
            _http = http;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<ServiceResponse<List<ActiveModuleDto>>> GetAllActiveModule()
        {
            var response = await _http.GetAsync("/api/Settings/GetActiveModules");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<List<ActiveModuleDto>> { Data = new List<ActiveModuleDto>(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var activeModules = JsonSerializer.Deserialize<List<ActiveModuleDto>>(content, _options);
                return new ServiceResponse<List<ActiveModuleDto>> { Data = activeModules, message = "success", statusCode = 200, status = true };
            }
        }

        public async Task<ServiceResponse<CompanyProfileDto>> GetCompanyInfo()
        {
            var response = await _http.GetAsync("/api/Settings/companyProfile");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<CompanyProfileDto> { Data = new CompanyProfileDto(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var activeModules = JsonSerializer.Deserialize<CompanyProfileDto>(content, _options);
                return new ServiceResponse<CompanyProfileDto> { Data = activeModules, message = "success", statusCode = 200, status = true };
            }
        }

        public async Task<ServiceResponse<SoftwareSettingsDto>> GetSoftwareSettingData()
        {
            var response = await _http.GetAsync("/api/Settings/softwareSettings");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<SoftwareSettingsDto> { Data = new SoftwareSettingsDto(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var activeModules = JsonSerializer.Deserialize<SoftwareSettingsDto>(content, _options);
                return new ServiceResponse<SoftwareSettingsDto> { Data = activeModules, message = "success", statusCode = 200, status = true };
            }
        }

        public async Task<ServiceResponse<CompanyProfileDto>> UpdateProfile(CompanyProfileDto companyProfile)
        {
            var response = await _http.PutAsJsonAsync("/api/settings/companyProfile", companyProfile);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<CompanyProfileDto> { Data = new CompanyProfileDto(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var roleDtos = JsonSerializer.Deserialize<CompanyProfileDto>(content, _options);
                return new ServiceResponse<CompanyProfileDto> { Data = roleDtos, message = "success", statusCode = ((int)response.StatusCode), status = true };
            }
        }

        public async Task<ServiceResponse<SoftwareSettingsDto>> UpdateSoftwareSetting(SoftwareSettingsDto softwareSettings)
{
            var response = await _http.PutAsJsonAsync("/api/Settings/softwareSettings", softwareSettings);

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<SoftwareSettingsDto> { Data = new SoftwareSettingsDto(), statusCode = ((int)response.StatusCode), status = false };
            }
            else
            {
                var roleDtos = JsonSerializer.Deserialize<SoftwareSettingsDto>(content, _options);
                return new ServiceResponse<SoftwareSettingsDto> { Data = roleDtos, message = "success", statusCode = ((int)response.StatusCode), status = true };
            }
        }
    }
}
