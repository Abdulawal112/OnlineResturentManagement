using Microsoft.AspNetCore.Components;
using OnlineResturnatManagement.Client.Helper;
using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Client.Services.Service;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Client.Pages.Setting
{
    public partial class SoftwareSetting
    {
        public SoftwareSettingsDto softwareSetting { get; set; }
        [Inject]
        public ISettingsHttpService SettingsHttpService { get; set; }
        StatusResult statusResult = new StatusResult();
        protected override async Task OnInitializedAsync()
        {
            softwareSetting = new SoftwareSettingsDto() { };
            await GetSoftwareSettingData();

        }

        private async Task GetSoftwareSettingData()
        {
            var response = await SettingsHttpService.GetSoftwareSettingData();
            statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
            if(response.Data != null)
            {
                softwareSetting = response.Data;
            }
            
        }

        public async Task UpdateCompany()
        {
            statusResult = new StatusResult();
            var response = await SettingsHttpService.UpdateSoftwareSetting(softwareSetting);
            statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
            if(statusResult.StatusCode == 200)
            {
                statusResult.Message = "Save Successfully";

            }
        }
    }
}
