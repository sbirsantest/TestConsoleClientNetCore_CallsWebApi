using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientNetCoreConsole
{
    public class TestPrinter : ITestPrinter
    {
        private readonly ILogger<TestPrinter> _logger;
        private readonly HttpClient _httpClient;

        public TestPrinter(ILogger<TestPrinter> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<string> GetWeatherForecastAsync()
        {
            _logger.LogInformation("Trying to get weather info...");

            if (_httpClient == null) throw new ArgumentNullException(nameof(_httpClient));

            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:5000/WeatherForecast");
                response.EnsureSuccessStatusCode();
                
                _logger.LogInformation("Successfully get weather info");

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error!");
            }

            return string.Empty;
        }
    }
}
