using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
using OnlineResturnatManagement.Client.Helper;

namespace OnlineResturnatManagement.Client.Pages.ShopSetup
{
    public partial class Kitchen : IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }
        [Inject]
        public IShopHttpService ShopHttpService { get; set; }

        public List<KitchenDto> kitchens = new List<KitchenDto>();
        public List<KitchenDto> MainKitchens = new List<KitchenDto>();
        KitchenDto editingKitchen = null;
        StatusResult statusResult = new StatusResult();

        string message = "";
        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            //Interceptor.RegisterEvent();
            await GetAllKitchen();
            StateHasChanged();

        }
        private async Task GetAllKitchen()
        {
            var result = await ShopHttpService.GetAllKitchen();
            kitchens = result.Data;
            MainKitchens = kitchens;

            if (result.status == false && result.statusCode == 403)
            {
                NavigationManager.NavigateTo("/error-403");
            }
            StateHasChanged();
        }
        private void CreateNewKitchen()
        {
            message = "";
            editingKitchen= new KitchenDto { IsNew = true, Editing = true };
            MainKitchens.Add(editingKitchen);
            StateHasChanged();

            //editingRole = UserHttpService.CreateRole(editingRole);
        }

        private void EditKitchen(KitchenDto kitchenDto)
        {
            message = "";
            kitchenDto.Editing = true;
            editingKitchen = kitchenDto;
        }

        private async Task UpdateKitchen()
        {
            statusResult = new StatusResult();
            if (editingKitchen.IsNew)
            {
                editingKitchen.CreateDate = DateTime.Now;
                editingKitchen.CreateBy = await GetCurrentUserNameAsync();
                var response = await ShopHttpService.CreateKitchen(editingKitchen);
                statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
                message = statusResult.Message;
                if (statusResult.Message == "" && statusResult.StatusCode == 201)
                {
                    statusResult.Message = "Save Successfully.";
                    await GetAllKitchen();
                    editingKitchen = new KitchenDto();
                }

            }
            else
            {
                editingKitchen.UpdateDate = DateTime.Now;
                editingKitchen.UpdateBy = await GetCurrentUserNameAsync();
                var response = await ShopHttpService.UpdateKitchen(editingKitchen);
                statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
                message = statusResult.Message;
                if (statusResult.Message == "" && statusResult.StatusCode == 200)
                {
                    statusResult.Message = "Update Successfully.";
                    await GetAllKitchen();
                    editingKitchen = new KitchenDto();
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
            editingKitchen = new KitchenDto();
            await GetAllKitchen();
        }
        public void Dispose() => Interceptor.DisposeEvent();
    }
}
