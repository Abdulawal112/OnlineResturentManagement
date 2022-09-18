using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace OnlineResturnatManagement.Client.Pages.TableManagement
{
    public partial class GetFloorManagement
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeAsync<IJSObjectReference>("import", "/monster-admin/js/TableManagement.js");
            await JSRuntime.InvokeAsync<IJSObjectReference>("import", "/monster-admin/js/DesignedTableManagement.js");

            StateHasChanged();
            //

        }
    }
}
