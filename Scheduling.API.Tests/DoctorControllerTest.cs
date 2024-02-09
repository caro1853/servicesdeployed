using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net;

namespace Scheduling.API.Tests
{
    public class DoctorControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public DoctorControllerTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Get_Request_Returns_Ok_Status_Code()
        {
            var response = await _client.GetAsync("api/v1/doctor");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


            // Verificar que la solicitud fue exitosa (código de estado 200 OK)
            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);


            //var c = _client.GetAsync("api/v1/doctor").Result;
            //c.EnsureSuccessStatusCode();

            //var r = c.Content.ReadAsByteArrayAsync().Result;
            //var respuestaConsulta = System.Text.Json.JsonSerializer.Deserialize<List<object>>(r);
        }

        // Otras pruebas de integración pueden ser agregadas aquí

        public void Dispose()
        {
            // Liberar recursos una vez que las pruebas han sido completadas
            _client.Dispose();
            _server.Dispose();
        }
    }
}

