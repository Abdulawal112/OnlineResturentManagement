﻿using OnlineResturnatManagement.Client.Services.IService;
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

        public async Task<ServiceResponse<CompanyProfileDto>> UpdateProfile(MultipartFormDataContent contentData)
        {
            var response = await _http.PostAsJsonAsync("/api/settings/", contentData);
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
    }
}
