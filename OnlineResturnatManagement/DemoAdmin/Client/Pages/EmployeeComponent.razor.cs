﻿using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Shared;
using Microsoft.AspNetCore.Components;
using System;
using OnlineResturnatManagement.Client.Services.IService;
using System.Net.NetworkInformation;

using OnlineResturnatManagement.Client.HttpRepository;

namespace OnlineResturnatManagement.Client.Pages
{
    public partial class EmployeeComponent : IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public IEmployeeHttpService EmployeeService { get; set; }

        public List<Employee> Employees = new List<Employee>();
        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            var result = await EmployeeService.GetAll();
            if(result.status==false && result.statusCode == 403)
            {
                NavigationManager.NavigateTo("/error-403");
            }
            Employees = result.Data;
        }
        public void Dispose() => Interceptor.DisposeEvent();
    }
}
