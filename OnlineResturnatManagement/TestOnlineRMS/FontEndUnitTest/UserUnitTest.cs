using Blazored.Toast.Services;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OnlineResturnatManagement.Client.Pages;
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
    public class UserUnitTest : TestContext
    {
        [Fact]
        public void CounterShouldIncrementWhenClicked()
        {
            
            var cut = RenderComponent<Counter>();

            // Act: find and click the <button> element to increment
            // the counter in the <p> element
            cut.Find("button").Click();

            // Assert: first find the <p> element, then verify its content
            cut.Find("p").MarkupMatches(@"<p role=""status"">Current count: 1</p>");
        }
        [Fact]
        public void TestComponentWithHttpClient()
        {

            var content = JsonSerializer.Serialize(new List<string> {});

            var mockHttp = new MockHttpMessageHandler();
            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7095");

            Services.AddSingleton(httpClient);

            mockHttp.When("/WeatherForecast")
                    .Respond(HttpStatusCode.OK, "application/json", content);

            var cut = RenderComponent<FetchData>();

            cut.WaitForAssertion(() => Assert.NotNull(cut.Instance.ToString()));
        }

    }
}
