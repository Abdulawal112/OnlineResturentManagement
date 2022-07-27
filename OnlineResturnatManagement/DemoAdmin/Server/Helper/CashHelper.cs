using OnlineResturnatManagement.Server.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace OnlineResturnatManagement.Server.Helper
{
    public class CashHelper<T> : ICashHelper<T> where T : class
    {
        private IDistributedCache _distributedCache;
        public CashHelper(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<List<T>> GetDataAsync(string cacheKey)
        {
            string dataList = await _distributedCache.GetStringAsync(cacheKey);

            var resultList = new List<T>();
            if (!string.IsNullOrEmpty(dataList))
            {
                resultList = JsonConvert.DeserializeObject<List<T>>(dataList);
            }
            return resultList;
        }

        public async Task<T> GetSingleDataAsync(string cacheKey)
        {
            string dataList =await _distributedCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(dataList))
            {
                return JsonConvert.DeserializeObject<T>(dataList);
            }
            return null;
        }

        public async void RemoveDataAsync(string cacheKey)
        {
            if (cacheKey != null)
            {
                await _distributedCache.RemoveAsync(cacheKey);
            }
        }
        public async void SetDataAsync(string cacheKey, List<T> values)
        {
            if (values != null)
            {
                var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(1))
                            .SetSlidingExpiration(TimeSpan.FromMinutes(1));
                await _distributedCache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(values), options);
            }
        }

        public async void SetDataAsync(string cacheKey, T value)
        {
            if (value != null)
            {
                var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(1))
                            .SetSlidingExpiration(TimeSpan.FromMinutes(1));
                await _distributedCache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(value), options);
            }
        }
    }
}
