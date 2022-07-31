using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Shared;
using Microsoft.AspNetCore.Components;
using System;
using OnlineResturnatManagement.Client.Services.IService;
using System.Net.NetworkInformation;

using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Shared.DTO;
using System.Data;
using OnlineResturnatManagement.Client.Helper;

namespace OnlineResturnatManagement.Client.Pages
{
    public partial class CreateUpdateRole : IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public IUserHttpService UserHttpService { get; set; }

        public List<RoleDto> Roles = new List<RoleDto>();
        public List<RoleDto> MainRoles = new List<RoleDto>();
        RoleDto editingRole = null;
        string message = "";
        protected override async Task OnInitializedAsync()
        {
            
            Interceptor.RegisterEvent();
            await GetAllRole();
            StateHasChanged();

        }

        private async Task  GetAllRole()
        {
            var result = await UserHttpService.GetRoles();
            Roles = result.Data;
            MainRoles = Roles;

            if (result.status == false && result.statusCode == 403)
            {
                NavigationManager.NavigateTo("/error-403");
            }
            StateHasChanged();
        }

        private void CreateNewRole()
        {
            message = "";
            editingRole = new RoleDto { IsNew = true, Editing = true };
            MainRoles.Add(editingRole);
            StateHasChanged();
            
            //editingRole = UserHttpService.CreateRole(editingRole);
        }

        private void EditRole(RoleDto roleDto)
        {
            message = "";
            roleDto.Editing = true;
            editingRole = roleDto;
        }

        private async Task UpdateRole()
        {

            if (editingRole.IsNew)
            {
                var response = await UserHttpService.CreateRole(editingRole);
                message = ResponseErrorMessage.GetErrorMessage(response.statusCode);
                if (message == "")
                {
                    await GetAllRole();
                    editingRole = new RoleDto();
                }

            }
            else
            {
                var response=await UserHttpService.UpdateRole(editingRole);
                message = ResponseErrorMessage.GetErrorMessage(response.statusCode);
                if (message == "")
                {
                    await GetAllRole();
                    editingRole = new RoleDto();
                }
                    
            }
            
            
           
            
        }

        private async Task CancelEditing()
        {
            editingRole = new RoleDto();
            await GetAllRole();
        }

        //private async Task DeleteCategory(int id)
        //{
        //    await CategoryService.DeleteCategory(id);
        //}




        public void Dispose() => Interceptor.DisposeEvent();
    }
}
