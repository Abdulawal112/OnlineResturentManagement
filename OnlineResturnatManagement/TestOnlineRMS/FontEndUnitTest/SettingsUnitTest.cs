using AngleSharp.Dom;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OnlineResturnatManagement.Client.Pages;
using OnlineResturnatManagement.Client.Pages.Setting;
using OnlineResturnatManagement.Client.Services.IService;
using OnlineResturnatManagement.Client.Services.Service;
using OnlineResturnatManagement.Shared;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestOnlineRMS.FontEndUnitTest
{
    public class SettingsUnitTest
    {
        [Fact]
        public void findDefaultWithoutStockSaleIsOffOrNot()
        {
            var content = JsonSerializer.Serialize(new List<string> { "data" });
            var mockHttp = new MockHttpMessageHandler();
            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7095");
            using var ctx = new TestContext();
            ctx.Services.AddSingleton(httpClient);
            mockHttp.When("/setting/software-setting")
                    .Respond(HttpStatusCode.OK, "application/json", content);

            var component = ctx.RenderComponent<SoftwareSetting>();
            component.WaitForAssertion(() => Assert.NotNull(component.Instance));
        }
    }
}
