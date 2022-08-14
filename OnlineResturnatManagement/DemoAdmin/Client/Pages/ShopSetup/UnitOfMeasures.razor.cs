using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
using OnlineResturnatManagement.Client.Helper;
using OnlineResturnatManagement.Client.Services.Service;

namespace OnlineResturnatManagement.Client.Pages.ShopSetup
{
    public partial class UnitOfMeasures : IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }
        [Inject]
        public IShopHttpService ShopHttpService { get; set; }

        public List<UnitOfMeasureDto> UOMs = new List<UnitOfMeasureDto>();
        public List<UnitOfMeasureDto> MainUOMs = new List<UnitOfMeasureDto>();
        UnitOfMeasureDto editingUMO = null;
        StatusResult statusResult = new StatusResult();

        string message = "";
        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            //Interceptor.RegisterEvent();
            await GetAllUOM();
            StateHasChanged();

        }
        private async Task GetAllUOM()
        {
            var result = await ShopHttpService.GetAllUOM();
            UOMs = result.Data;
            MainUOMs = UOMs;

            if (result.status == false && result.statusCode == 403)
            {
                NavigationManager.NavigateTo("/error-403");
            }
            StateHasChanged();
        }
        private void CreateNewUOM()
        {
            message = "";
            editingUMO = new UnitOfMeasureDto { IsNew = true, Editing = true };
            MainUOMs.Add(editingUMO);
            StateHasChanged();

            //editingRole = UserHttpService.CreateRole(editingRole);
        }

        private void EditUOM(UnitOfMeasureDto uomDto)
        {
            message = "";
            uomDto.Editing = true;
            editingUMO = uomDto;
        }

        private async Task UpdateUOM()
        {
            statusResult = new StatusResult();
            if (editingUMO.IsNew)
            {
                editingUMO.CreateDate = DateTime.Now;
                editingUMO.CreateBy = await GetCurrentUserNameAsync();
                var response = await ShopHttpService.CreateUOM(editingUMO);
                statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
                message = statusResult.Message;
                if (statusResult.Message == "" && statusResult.StatusCode == 201)
                {
                    statusResult.Message = "Save Successfully.";
                    await GetAllUOM();
                    editingUMO = new UnitOfMeasureDto();
                }

            }
            else
            {
                editingUMO.UpdateDate = DateTime.Now;
                editingUMO.UpdateBy = await GetCurrentUserNameAsync();
                var response = await ShopHttpService.UpdateUOM(editingUMO);
                statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
                message = statusResult.Message;
                if (statusResult.Message == "" && statusResult.StatusCode == 200)
                {
                    statusResult.Message = "Update Successfully.";
                    await GetAllUOM();
                    editingUMO = new UnitOfMeasureDto();
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
            editingUMO = new UnitOfMeasureDto();
            await GetAllUOM();
        }
        public void Dispose() => Interceptor.DisposeEvent();
    }
}
