using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OnlineResturnatManagement.Client.Pages.Setting;
using OnlineResturnatManagement.Shared.DTO;
using System.Reflection.Metadata;

namespace OnlineResturnatManagement.Client.Pages.TableManagement
{
    public partial class FloorManagement
    {
        private string imgUrl;
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //Interceptor.RegisterEvent();
            await JSRuntime.InvokeAsync<IJSObjectReference>("import", "/monster-admin/js/TableManagement.js");
            StateHasChanged();
            //

        }
        public async Task Call(string mydata)
        {
            Console.WriteLine(mydata);
            await JSRuntime.InvokeVoidAsync("dragElement", mydata);
        }

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
  
            IBrowserFile imgFile = e.File;
            var buffers = new byte[imgFile.Size];
            await imgFile.OpenReadStream().ReadAsync(buffers);
            string imageType = imgFile.ContentType;
            imgUrl = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";
            await imgFile.OpenReadStream().ReadAsync(buffers);
            this.StateHasChanged();
        }
    }
}
