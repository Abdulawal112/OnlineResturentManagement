namespace OnlineResturnatManagement.Server.Helper
{
    public interface ICashHelper
    {
        
        /// <summary>
        /// Get Data using key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetData<T>(string key);

        /// <summary>
        /// Set Data with Value and Expiration Time of Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        bool SetData<T>(string key, T value); //DateTimeOffset expirationTime

        /// <summary>
        /// Remove Data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        void RemoveData(string key);


        //For Reddis Cache

        //public Task<List<T>> GetDataAsync(string cacheKey);
        //public Task<T> GetSingleDataAsync(string cacheKey);
        //public void SetDataAsync(string cacheKey,List<T> values);
        //public void SetDataAsync(string cacheKey, T values);
        //public void RemoveDataAsync(string cacheKey);
    }
}
