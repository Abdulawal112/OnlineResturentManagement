using OnlineResturnatManagement.Server.Models;

namespace OnlineResturnatManagement.Server.Services.IService
{
    public interface ISettingSrevice
    {
        public Task<List<ActiveModule>> GetActiveModules();
    }
}
