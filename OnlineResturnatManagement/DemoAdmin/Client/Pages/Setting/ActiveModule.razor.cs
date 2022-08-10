using Microsoft.AspNetCore.Components;
using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Client.Services.Service;
using OnlineResturnatManagement.Shared.DTO;
using System.Data;

namespace OnlineResturnatManagement.Client.Pages.Setting
{
    public partial class ActiveModule :IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public ISettingsHttpService SettingsHttpService { get; set; }

        public List<ActiveModuleDto> ActiveModules { get; set; }
        protected override async Task OnInitializedAsync()
        {

            Interceptor.RegisterEvent();
            await GetActiveModule();
            StateHasChanged();

        }

        private async Task GetActiveModule()
        {
            var result = await SettingsHttpService.GetAllActiveModule();
            ActiveModules = result.Data;

           
            StateHasChanged();
        }

        public void Dispose() => Interceptor.DisposeEvent();
    }
}
