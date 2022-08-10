using OnlineResturnatManagement.Client.Pages;
using OnlineResturnatManagement.Client.Pages.Setting;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Client.Services.IService
{
    public interface ISettingsHttpService
    {
        public Task<ServiceResponse<List<ActiveModuleDto>>> GetAllActiveModule();
        public Task<ServiceResponse<CompanyProfileDto>> GetCompanyInfo();
        public Task<ServiceResponse<CompanyProfileDto>> UpdateProfile(CompanyProfileDto companyProfile);
        public Task<ServiceResponse<SoftwareSettingsDto>> GetSoftwareSettingData();
        public Task<ServiceResponse<SoftwareSettingsDto>> UpdateSoftwareSetting(SoftwareSettingsDto softwareSettings);
    }
}
