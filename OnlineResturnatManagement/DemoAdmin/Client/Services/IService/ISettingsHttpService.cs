using OnlineResturnatManagement.Client.Pages;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Client.Services.IService
{
    public interface ISettingsHttpService
    {
        public Task<ServiceResponse<List<ActiveModuleDto>>> GetAllActiveModule();
    }
}
