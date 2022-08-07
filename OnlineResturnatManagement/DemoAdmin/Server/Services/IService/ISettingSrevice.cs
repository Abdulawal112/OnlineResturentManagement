using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Server.Services.IService
{
    public interface ISettingSrevice
    {
        public Task<List<ActiveModule>> GetActiveModules();
        Task<CompanyProfileDto> SaveCompanyProfile(CompanyProfile companyInfo);
        Task<CompanyProfileDto> GetCompanyProfile();
    }
}
