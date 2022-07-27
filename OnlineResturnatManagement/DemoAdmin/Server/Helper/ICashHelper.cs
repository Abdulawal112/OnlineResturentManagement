namespace OnlineResturnatManagement.Server.Helper
{
    public interface ICashHelper<T>
    {
        public Task<List<T>> GetDataAsync(string cacheKey);
        public Task<T> GetSingleDataAsync(string cacheKey);
        public void SetDataAsync(string cacheKey,List<T> values);
        public void SetDataAsync(string cacheKey, T values);
        public void RemoveDataAsync(string cacheKey);
    }
}
