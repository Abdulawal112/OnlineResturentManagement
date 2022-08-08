using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OnlineResturnatManagement.Client.Helper;
using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using static System.Net.WebRequestMethods;

namespace OnlineResturnatManagement.Client.Pages.Setting
{
    public partial class CompanyProfile
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public ISettingsHttpService SettingsHttpService { get; set; }
        public CompanyProfileDto companyProfile { get; set; }
        //image
        private StreamContent fileContent;
        private string fileName;
        private string imgUrl;
        private string imageType;
        StatusResult statusResult = new StatusResult();
        string imageMessage = "";

        protected override async Task OnInitializedAsync()
        {
            companyProfile = new CompanyProfileDto();
            //Interceptor.RegisterEvent();;
           await GetCompanyProfile();

        }

        private async Task GetCompanyProfile()
        {
            var response = await SettingsHttpService.GetCompanyInfo();
            statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
            companyProfile = response.Data;
            imgUrl = $"data:Image/jpeg;base64,{Convert.ToBase64String(companyProfile.File.Data)}";
        }

        public async Task UpdateCompany() 
        {
            statusResult = new StatusResult();
            var response = await SettingsHttpService.UpdateProfile(companyProfile);
            statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
            statusResult.Message = "Save Successfully";
        }
       
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            imageMessage = "";
             IBrowserFile imgFile = e.File;
            var buffers = new byte[imgFile.Size];
            await imgFile.OpenReadStream().ReadAsync(buffers);
            string imageType = imgFile.ContentType;
            imgUrl = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";
            if(imgFile.Size / (1024 * 1024) < 5)
            {
                companyProfile.File = new FileData
                {
                    Data = buffers,
                    FileType = imageType,
                    Size = imgFile.Size,
                };
            }
            else
            {
                imageMessage = "Size is too big.image size must be lest then 5mb.";
            }
           
            this.StateHasChanged();
        }
    }
}
