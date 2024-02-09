using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http.Formatting;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Scheduling.API.Tests
{
    public class LoginControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public LoginControllerTest()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddInMemoryCollection(
                new Dictionary<string, string>
                {
                    {"keyjwt", "keysecretdkakdlkaldkaloeoeiakkjdfaksdjfkajhdfkjasdhf"}
                })
            .Build();

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(configuration));
            _client = _server.CreateClient();
        }

        [Fact]
        public void Post_Request_Returns_BadRequest_Status_Code()
        {
            var authInfo = new
            {
                email = "caro@gmail.com",
                password = "123456"
            };
            HttpResponseMessage response = null;
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(authInfo), Encoding.UTF8, "application/json");
                response = _client.PostAsync("login", content).Result;
                response.EnsureSuccessStatusCode();
                Assert.True(false, "Debería responder con error.");
            }
            catch (Exception ex)
            {
                Assert.True(response?.StatusCode == System.Net.HttpStatusCode.BadRequest);
                var r = System.Text.Json.JsonSerializer
                .Deserialize<object>(response.Content.ReadAsStringAsync().Result);
                Assert.True(r?.ToString().Contains("Usuario inválido"));
            }
        }

        [Fact]
        public void Post_Request_Returns_Ok_Status_Code()
        {
            //doctor
            var doctor = new
            {
                Name = "Maria",
                Email = "doctormaria@gmail.com",
                Especiality = "Odontóloga",
                Description = "Especialidad en ortodoncia"
            };

            //Create doctor
            var authInfo = new
            {
                email = "doctormaria@gmail.com",
                password = "123456"
            };

            var contentdoctor = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");
            var content = new StringContent(JsonConvert.SerializeObject(authInfo), Encoding.UTF8, "application/json");

            var rescreatedoctor = _client.PostAsync("api/v1/doctor", contentdoctor).Result;
            rescreatedoctor.EnsureSuccessStatusCode();

            var reslogin = _client.PostAsync("login", content).Result;
            reslogin.EnsureSuccessStatusCode();

            var r = System.Text.Json.JsonSerializer
                .Deserialize<object>(reslogin.Content.ReadAsStringAsync().Result);

            Assert.True(reslogin.StatusCode == System.Net.HttpStatusCode.OK);

            Assert.True(r?.ToString().Contains("token"));
        }

    }
}

