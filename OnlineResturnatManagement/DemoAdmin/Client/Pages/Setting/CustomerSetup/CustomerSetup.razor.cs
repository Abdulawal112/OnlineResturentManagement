using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using OnlineResturnatManagement.Client.Helper;
using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Client.Pages.Setting.CustomerSetup
{
    public partial class CustomerSetup:IDisposable
    {
        [Inject]
        public HttpInterceptorService HttpInterceptorService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ISettingsHttpService SettingsHttpService { get; set; }
        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }
        public List<CustomerSetupDtos> Customers = new List<CustomerSetupDtos>();
        public CustomerSetupDtos CustomerSetupDtos = new CustomerSetupDtos();
        StatusResult StatusResult = new StatusResult();
        string resultMessage = "";
        string Search = "";

        protected async override Task OnInitializedAsync()
        {
            HttpInterceptorService.RegisterEvent();
            await GetCustomers();

        }
        public async Task GetCustomers()
        {
            var result = await SettingsHttpService.GetAllCustomers();
            if (result.status == false && result.statusCode == 403)
            {
                NavigationManager.NavigateTo("/error-403");
            }
            Customers=result.Data;
        }

        async void EditCustomer(int customerId)
        {
            CustomerSetupDtos = new CustomerSetupDtos();
            StatusResult = new StatusResult();
            var result = await SettingsHttpService.GetCustomer(customerId);
            CustomerSetupDtos = result.Data;
            StateHasChanged();
        }

        async void UpdateCustomer()
        {
            var response = await SettingsHttpService.UpdateCustomerInfo(CustomerSetupDtos);
            StatusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
            if (StatusResult.Message == "" && StatusResult.StatusCode == 200)
            {
                StatusResult.Message = "Save Successfully.";
                await GetCustomers();
                CustomerSetupDtos = response.Data;
            }

            StateHasChanged();
        }

        public void Dispose() => HttpInterceptorService.DisposeEvent();
    }
}
