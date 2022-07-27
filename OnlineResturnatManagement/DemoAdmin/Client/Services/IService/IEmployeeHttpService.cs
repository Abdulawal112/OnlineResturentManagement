using OnlineResturnatManagement.Shared;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Client.Services.IService
{
    public interface IEmployeeHttpService
    {
        public Task<ServiceResponse<List<Employee>>> GetAll();
    
    }
}
