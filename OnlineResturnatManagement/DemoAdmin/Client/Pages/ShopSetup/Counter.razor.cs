using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
using OnlineResturnatManagement.Client.Helper;
using System.Net;

namespace OnlineResturnatManagement.Client.Pages.ShopSetup
{
    public partial class Counter : IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }
        [Inject]
        public IShopHttpService ShopHttpService { get; set; }

        public List<CounterInfoDto> Counters = new List<CounterInfoDto>();
        public List<CounterInfoDto> MainCounters = new List<CounterInfoDto>();
        CounterInfoDto editingCounter = null;
        StatusResult statusResult = new StatusResult();

        string message = "";
        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            //Interceptor.RegisterEvent();
            await GetAllCounter();
            StateHasChanged();

        }
        private async Task GetAllCounter()
        {
            var result = await ShopHttpService.GetAllCounter();
            Counters = result.Data;
            MainCounters = Counters;

            if (result.status == false && result.statusCode == 403)
            {
                NavigationManager.NavigateTo("/error-403");
            }
            StateHasChanged();
        }
        private void CreateNewCounter()
        {
            message = "";
            statusResult = new StatusResult();
            editingCounter = new CounterInfoDto { IsNew = true, Editing = true };
            MainCounters.Add(editingCounter);
            StateHasChanged();

            //editingRole = UserHttpService.CreateRole(editingRole);
        }

        private void EditCounter(CounterInfoDto counterInfoDto)
        {
            message = "";
            statusResult = new StatusResult();
            counterInfoDto.Editing = true;
            editingCounter = counterInfoDto;
        }

        private async Task UpdateCounter()
        {
            //editingCounter.MacAddress = System.Net.Dns.GetHostName();
            if (editingCounter.IsNew)
            {
               
                var response = await ShopHttpService.CreateCounter(editingCounter);
                statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
                message = statusResult.Message;
                if (statusResult.Message == "" && statusResult.StatusCode == 201)
                {
                    statusResult.Message = "Save Successfully.";
                    await GetAllCounter();
                    editingCounter = new CounterInfoDto();
                }

            }
            else
            {
                editingCounter.UpdateDate = DateTime.Now;
                editingCounter.UpdateBy = await GetCurrentUserNameAsync();
                var response = await ShopHttpService.UpdateCounter(editingCounter);
                statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
                message = statusResult.Message;
                if (statusResult.Message == "" && statusResult.StatusCode == 200)
                {
                    statusResult.Message = "Update Successfully.";
                    await GetAllCounter();
                    editingCounter = new CounterInfoDto();
                }

            }

        }
        private async Task<string> GetCurrentUserNameAsync()
        {
            var authstate = await GetAuthenticationStateAsync.GetAuthenticationStateAsync();
            var user = authstate.User;
            var name = user.Identity.Name;
            return name;
        }
        private async Task CancelEditing()
        {
            editingCounter = new CounterInfoDto();
            await GetAllCounter();
        }
        
        
        public void Dispose() => Interceptor.DisposeEvent();
    }
}
