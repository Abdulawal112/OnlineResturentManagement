using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Client.Services.IService;
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
        public List<NavigationMenuDto> NavigationMenus { get; set; }

        StatusResult statusResult = new StatusResult();
        
        protected override async Task OnInitializedAsync()
        {
           
           
            Interceptor.RegisterEvent();

            var authstate = await GetAuthenticationStateAsync.GetAuthenticationStateAsync();
            var user = authstate.User;
            var name = user.Identity.Name;

            await GetNavigationMenu(name);
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

        public void Dispose() => Interceptor.DisposeEvent();
    }
}
