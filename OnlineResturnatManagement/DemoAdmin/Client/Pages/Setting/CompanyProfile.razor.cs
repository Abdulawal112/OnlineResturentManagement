using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
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

        protected override async Task OnInitializedAsync()
        {
            companyProfile = new CompanyProfileDto();
            //Interceptor.RegisterEvent();

        }
        public async Task UpdateCompany() 
        {
            // Provides a container for content encoded using multipart/form-data MIME type.
            using var content = new MultipartFormDataContent();
            content.Add
            (content: fileContent, name: "\"file\"", fileName: fileName);

            var response = await SettingsHttpService.UpdateProfile(content);

        }
       
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            // setting the max size for the file 
            long maxFileSize = 1024 * 1024 * 10;
            // Provide the HTTP Content based Stream
            // and open the stream for reading the uploaded file
            fileContent = new StreamContent(e.File.OpenReadStream(maxFileSize));
            // read file name
            fileName = e.File.Name;

            var fileSize = new byte[e.File.Size];
            // read the file bytes in sequence
            await e.File.OpenReadStream().ReadAsync(fileSize);
            // read file content type
            imageType = e.File.ContentType;
            // create URL
            imgUrl = $"data:{imageType};base64,{Convert.ToBase64String(fileSize)}";
            
            this.StateHasChanged();
        }
    }
}
