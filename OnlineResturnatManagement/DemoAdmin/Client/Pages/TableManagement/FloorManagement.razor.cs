using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Reflection.Metadata;

namespace OnlineResturnatManagement.Client.Pages.TableManagement
{
    public partial class FloorManagement
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //Interceptor.RegisterEvent();

            await JSRuntime.InvokeAsync<IJSObjectReference>("import","/monster-admin/js/MyCustom.js");
            StateHasChanged();
            //

        }
        public async Task Call(string mydata)
        {
            Console.WriteLine(mydata);
            await JSRuntime.InvokeVoidAsync("dragElement", mydata);
        }
    }
}
