using BlazorAppWebAssembly.Client.Services.Service;
using Blazored.LocalStorage;
using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Client;
using OnlineResturnatManagement.Client.AuthProviders;
using OnlineResturnatManagement.Client.HttpRepository;
using OnlineResturnatManagement.Client.Services.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using OnlineResturnatManagement.Client.Services.Service;
using Blazored.Toast;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5011/api/") });
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddScoped<HttpInterceptorService>();
builder.Services.AddScoped<IEmployeeHttpService, EmployeeHttpService>();
builder.Services.AddScoped<IUserHttpService, UserHttpService>();
builder.Services.AddScoped<ISettingsHttpService, SettingsHttpService>();

builder.Services.AddBlazoredToast();



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }.EnableIntercept(sp));

builder.Services.AddHttpClientInterceptor();

await builder.Build().RunAsync();
