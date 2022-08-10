using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using OnlineResturnatManagement.Client.Helper;
using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Client.Pages.Setting;
using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Client.Services.Service;
using OnlineResturnatManagement.Shared.DTO;
using System;
using System.Net.NetworkInformation;

namespace OnlineResturnatManagement.Client.Shared
{
    public partial class MainLayout : IDisposable
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public IUserHttpService UserService { get; set; }
        [Inject]
        public ISettingsHttpService SettingsHttpService { get; set; }
        public List<NavigationMenuDto> NavigationMenus { get; set; }

        StatusResult statusResult = new StatusResult();
        private string imgUrl;


        protected override async Task OnInitializedAsync()
        {
            //Interceptor.RegisterEvent();

            var authstate = await GetAuthenticationStateAsync.GetAuthenticationStateAsync();
            var user = authstate.User;
            var name = user.Identity.Name;

            await GetNavigationMenu(name);
            await GetCompanyProfile();
            await JSRuntime.InvokeAsync<IJSObjectReference>("import", "/monster-admin/js/custom.js");
            await JSRuntime.InvokeAsync<IJSObjectReference>("import", "/monster-admin/assets/plugins/styleswitcher/jQuery.style.switcher.js");
            StateHasChanged();
            //


        }
        
        private async Task GetNavigationMenu(string name)
        {
            var response = await UserService.GetUserMenu(name);
            NavigationMenus = response.Data;
            StateHasChanged();
        }
        private async Task GetCompanyProfile()
        {
            var response = await SettingsHttpService.GetCompanyInfo();
            statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
            imgUrl = $"data:Image/jpeg;base64,{Convert.ToBase64String(response.Data.File.Data)}";
        }
        public void Dispose() => Interceptor.DisposeEvent();
    }
}
