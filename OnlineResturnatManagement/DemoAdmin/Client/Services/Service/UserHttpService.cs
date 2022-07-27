﻿using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Shared;
using OnlineResturnatManagement.Shared.DTO;
using System.Net.Http.Json;
using System.Text.Json;

namespace OnlineResturnatManagement.Client.Services.Service
{
    public class UserHttpService : IUserHttpService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _options;
        //public List<RoleDto> Roles { get; set; } = new List<RoleDto>();


        public UserHttpService(HttpClient http)
        {
            _http = http;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        
        public async Task<ServiceResponse<List<UserDto>>> GetAllUser()
        {
            var response = await _http.GetAsync("/api/users");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<List<UserDto>> { Data = new List<UserDto>(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var users = JsonSerializer.Deserialize<List<UserDto>>(content, _options);
                return new ServiceResponse<List<UserDto>> { Data = users, message = "success", statusCode = 200, status = true };
            }
        }
        public async Task<ServiceResponse<List<RoleDto>>> GetRoles()
        {
            var response = await _http.GetAsync("/api/users/GetRoles");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<List<RoleDto>> { Data = new List<RoleDto>(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var users = JsonSerializer.Deserialize<List<RoleDto>>(content, _options);
                
                return new ServiceResponse<List<RoleDto>> { Data = users, message = "success", statusCode = ((int)response.StatusCode), status = true };
                
            }
           
        }
        public async Task<ServiceResponse<bool>> CreateRole(RoleDto roleDto)
        {
            var response = await _http.PostAsJsonAsync("/api/users/CreateRole",roleDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<bool> { Data = false, statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
               
                return new ServiceResponse<bool> { Data = true, message = "success", statusCode = ((int)response.StatusCode), status = true };
            }
        }
        public async Task<ServiceResponse<bool>> UpdateRole(RoleDto roleDto)
        {
            var response = await _http.PutAsJsonAsync("/api/users/UpdateRole", roleDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<bool> { Data = false, statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {

                return new ServiceResponse<bool> { Data = true, message = "success", statusCode = ((int)response.StatusCode), status = true };
            }
        }
    }
}
