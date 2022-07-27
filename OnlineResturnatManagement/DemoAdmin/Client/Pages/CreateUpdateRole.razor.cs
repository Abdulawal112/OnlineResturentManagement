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
        protected override async Task OnInitializedAsync()
        {
            
            Interceptor.RegisterEvent();
            await GetAllRole();
            
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
           
            editingRole = new RoleDto { IsNew = true, Editing = true };
            MainRoles.Add(editingRole);
            StateHasChanged();
            //editingRole = UserHttpService.CreateRole(editingRole);
        }

        private void EditRole(RoleDto roleDto)
        {
            roleDto.Editing = true;
            editingRole = roleDto;
        }

        private async Task UpdateRole()
        {
            if (editingRole.IsNew)
                await UserHttpService.CreateRole(editingRole);
            else
                await UserHttpService.UpdateRole(editingRole);
            await GetAllRole();
            editingRole = new RoleDto();
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
