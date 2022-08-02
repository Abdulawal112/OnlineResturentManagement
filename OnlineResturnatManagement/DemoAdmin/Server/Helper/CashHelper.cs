using OnlineResturnatManagement.Server.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Server.Helper
{
    public class CashHelper<T> : ICashHelper<T> where T : class
    {
        private IDistributedCache _distributedCache;
        private readonly IMemoryCache _memoryCache;

        public CashHelper(IDistributedCache distributedCache, IMemoryCache memoryCache)
        {
            _distributedCache = distributedCache;
            _memoryCache = memoryCache;
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

        /*public void SetInMemoryCache(string cacheKey, IEnumerable<T> values)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(100))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
            _memoryCache.Set(cacheKey, values, cacheEntryOptions);
        }*/
    }
}
