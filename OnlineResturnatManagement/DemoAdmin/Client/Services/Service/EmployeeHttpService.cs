
using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Shared;
using OnlineResturnatManagement.Shared.DTO;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorAppWebAssembly.Client.Services.Service
{
    public class EmployeeHttpService : IEmployeeHttpService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _options;
        public EmployeeHttpService(HttpClient http)
        {
            _http = http;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<ServiceResponse<List<Employee>>> GetAll()
        {
            //var response = await _http.GetFromJsonAsync<List<Employee>>("/api/employees");
            //return response;
           
            var response = await _http.GetAsync("/api/employees");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<List<Employee>> { Data = new List<Employee>(), statusCode= ((int)response.StatusCode), status = false };

            }
            else
            {
                var employees = JsonSerializer.Deserialize<List<Employee>>(content, _options);
                return new ServiceResponse<List<Employee>> { Data = employees, message = "success", statusCode=200, status = true };
            }
            
        }
    }
}
