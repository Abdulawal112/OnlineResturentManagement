using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Shared;
using Microsoft.AspNetCore.Components;
using System;
using OnlineResturnatManagement.Client.Services.IService;
using System.Net.NetworkInformation;

using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Shared.DTO;
using OnlineResturnatManagement.Client.Services.Service;
using OnlineResturnatManagement.Client.Helper;

namespace OnlineResturnatManagement.Client.Pages
{
    public partial class EditRolePermission : IDisposable
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public IUserHttpService UserService { get; set; }
        StatusResult statusResult = new StatusResult();
        protected List<string> SelectedIds = new List<string>();
        public List<NavigationMenuDto> NavigationMenuDtos = new List<NavigationMenuDto>();
        public List<NavigationMenuDto> RoleNavigationMenuDtos = new List<NavigationMenuDto>();
        public string RoleName { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            //var result = await EmployeeService.GetAll();
            //if(result.status==false && result.statusCode == 403)
            //{
            //    NavigationManager.NavigateTo("/error-403");
            //}
            //Employees = result.Data;
            await GetInitialData();
        }

        private async Task GetInitialData()
        {
            RoleName = "";
            var response = await UserService.GetMenuByRoleId(Id);
            RoleNavigationMenuDtos = response.Data;
            var response2 = await UserService.GetAllMenu();
            NavigationMenuDtos = response2.Data;
            SelectedIds = new List<string>();
            RoleName = RoleNavigationMenuDtos[0].RoleName;
            if (RoleNavigationMenuDtos != null && RoleNavigationMenuDtos.Count > 0)
            {
                foreach (var item in RoleNavigationMenuDtos)
                {
                    SelectedIds.Add(item.Id.ToString());
                }
            }
            StateHasChanged();

        }

        private async Task UpdateNavigationRole()
        {
            RoleNavigationMenuDtos = new List<NavigationMenuDto>();
            if (SelectedIds.Count > 0)
            {
                foreach (var item in SelectedIds)
                {
                    RoleNavigationMenuDtos.Add(new NavigationMenuDto { Id = Convert.ToInt32(item), Name = "", ParentMenuId = 0 }); ;
                }
            }
            var result = RoleNavigationMenuDtos;
            if (RoleNavigationMenuDtos.Count > 0)
            {
                var response = await UserService.UpdateRoleMenus(Id, RoleNavigationMenuDtos);
                statusResult = ResponseErrorMessage.GetErrorMessage(response.statusCode);
                if (statusResult.Message == "" && statusResult.StatusCode == 200)
                {
                    statusResult.Message = "Save Successfully.";
                    
                }

               // StateHasChanged();
            }
        }
        public void Dispose() => Interceptor.DisposeEvent();
    }
}
