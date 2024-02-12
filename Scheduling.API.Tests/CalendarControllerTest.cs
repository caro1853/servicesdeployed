using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Scheduling.API.Models;

namespace Scheduling.API.Tests
{
	public class CalendarControllerTest: BaseControllerTest
    {
        
        public CalendarControllerTest():base()
		{
            
        }
        /*
        [Fact]
        public async Task Getoperationalhours_Returns_Unauthorized()
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync("api/v1/calendar/getoperationalhours/1");
                
                response.EnsureSuccessStatusCode();
            }
            catch(Exception ex)
            {
                Assert.True(response?.StatusCode == System.Net.HttpStatusCode.Unauthorized);
            }
        }

        [Fact]
        public async Task Getoperationalhours_Returns_Ok()
        {
            HttpResponseMessage response = null;

            string token = await GetToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await _client.GetAsync("api/v1/calendar/getoperationalhours/1");

            response.EnsureSuccessStatusCode();
            Assert.True(response?.StatusCode == System.Net.HttpStatusCode.OK);
        }
        */

    }
}

