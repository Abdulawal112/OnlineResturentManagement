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
        //customerSetup
        public Task<ServiceResponse<List<CustomerSetupDtos>>> GetAllCustomers();
        public Task<ServiceResponse<CustomerSetupDtos>> GetCustomer(int customerId);
        public Task<ServiceResponse<CustomerSetupDtos>> UpdateCustomerInfo(CustomerSetupDtos requestCustomer);

        //creditSetup
        public Task<ServiceResponse<List<CreditCardDtos>>> GetAllCreditsInfo();
        public Task<ServiceResponse<CreditCardDtos>> GetCreditCardById(int creditCardId);
        Task<ServiceResponse<CreditCardDtos>> UpdateCreditCardInfo(CreditCardDtos creditCardDtos);
    }
}
