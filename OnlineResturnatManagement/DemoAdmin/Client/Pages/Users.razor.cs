using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Shared;
using Microsoft.AspNetCore.Components;
using System;
using OnlineResturnatManagement.Client.Services.IService;
using System.Net.NetworkInformation;

using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Shared.DTO;

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
        public UserDto userDto = new UserDto();
        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            var result = await UserService.GetAllUser();
            if(result.status==false && result.statusCode == 403)
            {
                NavigationManager.NavigateTo("/error-403");
            }
            UserDtos = result.Data;
        }
        async void EditUser(int userId)
        {
            //var result =  await UserService.GetUserById(userId);
            //userDto = result.Data;
            //NavigationManager.NavigateTo($"user/{userId}");
        }


        public void Dispose() => Interceptor.DisposeEvent();
    }
}
