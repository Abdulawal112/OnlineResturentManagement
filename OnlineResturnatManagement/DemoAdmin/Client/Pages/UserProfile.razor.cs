using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Shared;
using Microsoft.AspNetCore.Components;
using System;
using OnlineResturnatManagement.Client.Services.IService;
using System.Net.NetworkInformation;

using OnlineResturnatManagement.Client.HttpRepository;
using Microsoft.AspNetCore.Components.Authorization;
using OnlineResturnatManagement.Shared.DTO;
using OnlineResturnatManagement.Client.Helper;
using System.Xml.Linq;

namespace OnlineResturnatManagement.Client.Pages
{
    public partial class UserProfile : IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }
        [Inject]
        public IUserHttpService UserService { get; set; }
        StatusResult statusResult = new StatusResult();


        public UserDto userDto = new UserDto();
        protected override async Task OnInitializedAsync()
        {
           
            var name =  await GetCurrentUserNameAsync();
            var response= await UserService.GetUserByName(name);
            userDto = response.Data;


        }
        private async Task<string> GetCurrentUserNameAsync()
        {
            var authstate = await GetAuthenticationStateAsync.GetAuthenticationStateAsync();
            var user = authstate.User;
            var name = user.Identity.Name;
            return name;
        }
        async void UpdateUser()
{
            userDto.UpdateBy = await GetCurrentUserNameAsync();
            userDto.UpdateDate = DateTime.Now;
            var response = await UserService.UpdateUserWithRole(userDto);
            statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
            if (statusResult.Message == "" && statusResult.StatusCode == 200)
            {
                statusResult.Message = "Save Successfully.";

                userDto = response.Data;
            }

            StateHasChanged();
        }
        public void Dispose() => Interceptor.DisposeEvent();
    }
}
