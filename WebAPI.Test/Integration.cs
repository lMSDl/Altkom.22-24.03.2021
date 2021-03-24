using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebAPI.Test.Controllers
{
    public class Integration : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public Integration(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/Orders")]
        [InlineData("/WeatherForecast")]
        public async Task Get(string url)
        {
            var clinet = _factory.CreateClient();

            var response = await clinet.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
