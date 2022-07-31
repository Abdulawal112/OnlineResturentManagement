using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Shared;
using Microsoft.AspNetCore.Components;
using System;
using OnlineResturnatManagement.Client.Services.IService;
using System.Net.NetworkInformation;

using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Shared.DTO;
using OnlineResturnatManagement.Client.Helper;

namespace OnlineResturnatManagement.Client.Pages
{
    public partial class Users : IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public IUserHttpService UserService { get; set; }


        public List<UserDto> UserDtos = new List<UserDto>();
        public List<RoleDto> RoleDtos = new List<RoleDto>();
        public UserDto userDto = new UserDto();
        StatusResult statusResult = new StatusResult();
        string resultMessage = "";
        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            await GetIntialData();

        }

        private async Task GetIntialData()
        {
            var result = await UserService.GetAllUser();
            if (result.status == false && result.statusCode == 403)
            {
                NavigationManager.NavigateTo("/error-403");
            }
            UserDtos = result.Data;
            var result2 = await UserService.GetRoles();
            RoleDtos = result2.Data;
        }

        async void EditUser(int userId)
        {
            statusResult = new StatusResult();
            var result = await UserService.GetUserById(userId);
            //var result =  await UserService.GetUserById(userId);
            userDto = result.Data;
            StateHasChanged();
            //NavigationManager.NavigateTo($"user/{userId}");
        }
        async void UpdateUser()
        {
            var response = await UserService.UpdateUserWithRole(userDto);
            statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
            if (statusResult.Message == "" && statusResult.StatusCode==200)
            {
                statusResult.Message = "Save Successfully.";
                await GetIntialData();
                userDto = new UserDto();
            }
            
            StateHasChanged();
        }

        public void Dispose() => Interceptor.DisposeEvent();
    }
}
