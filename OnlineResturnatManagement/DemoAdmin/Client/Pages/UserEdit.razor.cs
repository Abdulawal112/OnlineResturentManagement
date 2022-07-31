using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Shared;
using Microsoft.AspNetCore.Components;
using System;
using OnlineResturnatManagement.Client.Services.IService;
using System.Net.NetworkInformation;

using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Shared.DTO;
using OnlineResturnatManagement.Client.Services.Service;

namespace OnlineResturnatManagement.Client.Pages
{
    public partial class UserEdit : IDisposable
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public IEmployeeHttpService EmployeeService { get; set; }
        public UserDto UserDto =new UserDto();

        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            //var result = await EmployeeService.GetAll();
            //if(result.status==false && result.statusCode == 403)
            //{
            //    NavigationManager.NavigateTo("/error-403");
            //}
            //Employees = result.Data;
        }

        private async Task UpdateRole()
        {
            
        }
        public void Dispose() => Interceptor.DisposeEvent();
    }
}
