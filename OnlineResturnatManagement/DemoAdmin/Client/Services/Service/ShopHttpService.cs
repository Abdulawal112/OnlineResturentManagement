using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
using System.Net.Http.Json;
using System.Text.Json;

namespace OnlineResturnatManagement.Client.Services.Service
{
    public class ShopHttpService : IShopHttpService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _options;

        public ShopHttpService(HttpClient http)
        {
            _http = http;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        
        //Get Counters
        public async Task<ServiceResponse<List<CounterInfoDto>>> GetAllCounter()
        {
            var response = await _http.GetAsync("/api/shops/GetCounters");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<List<CounterInfoDto>> { Data = new List<CounterInfoDto>(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var result = JsonSerializer.Deserialize<List<CounterInfoDto>>(content, _options);
                return new ServiceResponse<List<CounterInfoDto>> { Data = result, message = "success", statusCode = 200, status = true };
            }
        }
        //Get Kitchens
        public async Task<ServiceResponse<List<KitchenDto>>> GetAllKitchen()
        {
            var response = await _http.GetAsync("/api/shops/GetKitchens");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<List<KitchenDto>> { Data = new List<KitchenDto>(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var result = JsonSerializer.Deserialize<List<KitchenDto>>(content, _options);
                return new ServiceResponse<List<KitchenDto>> { Data = result, message = "success", statusCode = 200, status = true };
            }
        }
        //Get UnitOfMeasures
        public async Task<ServiceResponse<List<UnitOfMeasureDto>>> GetAllUOM()
        {
            var response = await _http.GetAsync("/api/shops/GetUnitOfMeasures");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<List<UnitOfMeasureDto>> { Data = new List<UnitOfMeasureDto>(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var result = JsonSerializer.Deserialize<List<UnitOfMeasureDto>>(content, _options);
                return new ServiceResponse<List<UnitOfMeasureDto>> { Data = result, message = "success", statusCode = 200, status = true };
            }
        }
        //Save Counter
        public async Task<ServiceResponse<CounterInfoDto>> CreateCounter(CounterInfoDto counterDto)
        {
            var response = await _http.PostAsJsonAsync("/api/shops/CreateCounter", counterDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<CounterInfoDto> { Data = new CounterInfoDto(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var roleDtos = JsonSerializer.Deserialize<CounterInfoDto>(content, _options);
                return new ServiceResponse<CounterInfoDto> { Data = roleDtos, message = "success", statusCode = ((int)response.StatusCode), status = true };
            }
        }
        //Save Kitchen
        public async Task<ServiceResponse<KitchenDto>> CreateKitchen(KitchenDto kitchenDto)
        {
            var response = await _http.PostAsJsonAsync("/api/shops/CreateKitchen", kitchenDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<KitchenDto> { Data = new KitchenDto(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var roleDtos = JsonSerializer.Deserialize<KitchenDto>(content, _options);
                return new ServiceResponse<KitchenDto> { Data = roleDtos, message = "success", statusCode = ((int)response.StatusCode), status = true };
            }
        }
        //Save UnitOfMeasure
        public async Task<ServiceResponse<UnitOfMeasureDto>> CreateUOM(UnitOfMeasureDto uomDto)
        {
            var response = await _http.PostAsJsonAsync("/api/shops/CreateUOM", uomDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<UnitOfMeasureDto> { Data = new UnitOfMeasureDto(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var roleDtos = JsonSerializer.Deserialize<UnitOfMeasureDto>(content, _options);
                return new ServiceResponse<UnitOfMeasureDto> { Data = roleDtos, message = "success", statusCode = ((int)response.StatusCode), status = true };
            }
        }
        //Update Counter
        public async Task<ServiceResponse<CounterInfoDto>> UpdateCounter(CounterInfoDto counterDto)
        {
            var response = await _http.PutAsJsonAsync("/api/shops/UpdateCounter", counterDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<CounterInfoDto> { Data = new CounterInfoDto(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var roleDtos = JsonSerializer.Deserialize<CounterInfoDto>(content, _options);
                return new ServiceResponse<CounterInfoDto> { Data = roleDtos, message = "success", statusCode = ((int)response.StatusCode), status = true };
            }
        }
        //Update Kitchen
        public async Task<ServiceResponse<KitchenDto>> UpdateKitchen(KitchenDto kitchenDto)
        {
            var response = await _http.PutAsJsonAsync("/api/shops/UpdateKitchen", kitchenDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<KitchenDto> { Data = new KitchenDto(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var roleDtos = JsonSerializer.Deserialize<KitchenDto>(content, _options);
                return new ServiceResponse<KitchenDto> { Data = roleDtos, message = "success", statusCode = ((int)response.StatusCode), status = true };
            }
        }
        //Update UnitOfMeasures
        public async Task<ServiceResponse<UnitOfMeasureDto>> UpdateUOM(UnitOfMeasureDto uomDto)
        {
            var response = await _http.PutAsJsonAsync("/api/shops/UpdateUOM", uomDto);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<UnitOfMeasureDto> { Data = new UnitOfMeasureDto(), statusCode = ((int)response.StatusCode), status = false };

            }
            else
            {
                var roleDtos = JsonSerializer.Deserialize<UnitOfMeasureDto>(content, _options);
                return new ServiceResponse<UnitOfMeasureDto> { Data = roleDtos, message = "success", statusCode = ((int)response.StatusCode), status = true };
            }
        }
    }
}
