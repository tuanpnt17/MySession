using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MySession.IntegrationTest
{
    public class MySessionIntegrationTest(WebApplicationFactory<Program> factory)
        : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient = factory.CreateClient();

        [Fact]
        public async Task Call_TestIndex_Returns_Ok_Async()
        {
            var response = await _httpClient.GetAsync($"Test/Index");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
