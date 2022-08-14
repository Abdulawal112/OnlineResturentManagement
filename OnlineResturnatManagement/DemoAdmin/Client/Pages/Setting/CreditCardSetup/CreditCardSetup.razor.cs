using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
using OnlineResturnatManagement.Client.Helper;

namespace OnlineResturnatManagement.Client.Pages.Setting.CreditCardSetup
{
    public partial class CreditCardSetup:IDisposable
    {
        [Inject]
        public HttpInterceptorService HttpInterceptorService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ISettingsHttpService SettingsHttpService { get; set; }
        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }
        public List<CreditCardDtos> CreditCards = new List<CreditCardDtos>();
        public CreditCardDtos CreditCardDtos = new CreditCardDtos();
        StatusResult StatusResult = new StatusResult();
        string resultMessage = "";
        string Search = "";

        protected async override Task OnInitializedAsync()
        {
            HttpInterceptorService.RegisterEvent();
            await GetCreditCards();

        }
        public async Task GetCreditCards()
        {
            var result = await SettingsHttpService.GetAllCreditsInfo();
            if (result.status == false && result.statusCode == 403)
            {
                NavigationManager.NavigateTo("/error-403");
            }
            CreditCards = result.Data;
        }

        async void EditCreditCard(int creditCardId)
        {
            CreditCardDtos = new CreditCardDtos();
            StatusResult = new StatusResult();
            var result = await SettingsHttpService.GetCreditCardById(creditCardId);
            CreditCardDtos = result.Data;
            StateHasChanged();
        }

        async void UpdateCreditCatdInfo()
        {
            var response = await SettingsHttpService.UpdateCreditCardInfo(CreditCardDtos);
            StatusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
            if (StatusResult.Message == "" && StatusResult.StatusCode == 200)
            {
                StatusResult.Message = "Save Successfully.";
                await GetCreditCards();
                CreditCardDtos = response.Data;
            }

            StateHasChanged();
        }
        public void Dispose() => HttpInterceptorService.DisposeEvent();
    }
}
