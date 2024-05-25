using LoupGarou.Controllers;
using LoupGarou.Model;
using LoupGarou.Model.Requests;
using LoupGarou.Services;
using LoupGarou.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TestLoupGarou
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            //// Arrange
            //var application = new LoupGarouWebApplicationFactory();
            //var client = application.CreateClient();

            //// Act
            //var response = await client.GetAsync("api/cards");

            //// Assert
            //response.EnsureSuccessStatusCode();

            // Arrange

            var webAppFactory = new WebApplicationFactory<Program>();
            var client = webAppFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost")
            });
        
            // Act
            var response = await client.GetAsync("api/cards");

            // Assert
            response.EnsureSuccessStatusCode();

        }
    }
}