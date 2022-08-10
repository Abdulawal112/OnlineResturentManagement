using OnlineResturnatManagement.Server.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Server.Helper
{
    public class CashHelper : ICashHelper
    {
        //private IDistributedCache _distributedCache; //For Reddis Cache
        private readonly IMemoryCache _memoryCache;

        public CashHelper( IMemoryCache memoryCache) //IDistributedCache distributedCache,
        {
            //_distributedCache = distributedCache;
            _memoryCache = memoryCache;
        }
        //ObjectCache _memoryCache = MemoryCache.Default;
        public T GetData<T>(string key)
        {
            try
            {
                T item = (T)_memoryCache.Get(key);
                return item;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool SetData<T>(string key, T value)
        {
            bool res = true;
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Set(key, value, expirationTime);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return res;
        }
        public void RemoveData(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Remove(key);
                }
            }
            catch (Exception e)
            {
                throw;
            }
           
        }
        
        //public async Task<List<T>> GetDataAsync(string cacheKey)
        //{
        //    string dataList = await _distributedCache.GetStringAsync(cacheKey);

        //    var resultList = new List<T>();
        //    if (!string.IsNullOrEmpty(dataList))
        //    {
        //        resultList = JsonConvert.DeserializeObject<List<T>>(dataList);
        //    }
        //    return resultList;
        //}

        //public async Task<T> GetSingleDataAsync(string cacheKey)
        //{
        //    string dataList =await _distributedCache.GetStringAsync(cacheKey);

        //    if (!string.IsNullOrEmpty(dataList))
        //    {
        //        return JsonConvert.DeserializeObject<T>(dataList);
        //    }
        //    return null;
        //}

        //public async void RemoveDataAsync(string cacheKey)
        //{
        //    if (cacheKey != null)
        //    {
        //        await _distributedCache.RemoveAsync(cacheKey);
        //    }
        //}
        //public async void SetDataAsync(string cacheKey, List<T> values)
        //{
        //    if (values != null)
        //    {
        //        var options = new DistributedCacheEntryOptions()
        //                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(1))
        //                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));
        //        await _distributedCache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(values), options);
        //    }
        //}

        //public async void SetDataAsync(string cacheKey, T value)
        //{
        //    if (value != null)
        //    {
        //        var options = new DistributedCacheEntryOptions()
        //                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(1))
        //                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));
        //        await _distributedCache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(value), options);
        //    }
        //}

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
