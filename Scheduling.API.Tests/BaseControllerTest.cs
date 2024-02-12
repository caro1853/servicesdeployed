using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Scheduling.API.Models;

namespace Scheduling.API.Tests
{
	public class BaseControllerTest
	{
        internal readonly TestServer _server;
        internal readonly HttpClient _client;

        public BaseControllerTest()
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

        internal async Task<string> GetToken()
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

            var jsonString = await reslogin.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseAuthentication>(jsonString);

            return response?.Token ?? string.Empty;
        }
    }
}

