using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientNetCoreConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            Task runHost = host.RunAsync();

            using var http = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient();
            await PrintWeatherForecastAsync(http);

            await runHost;
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices(services => 
                services.AddHttpClient()
                );

        private static async Task PrintWeatherForecastAsync(HttpClient httpClient)
        {
            if (httpClient == null) throw new ArgumentNullException(nameof(httpClient));

            try
            {
                var response = await httpClient.GetAsync($"http://localhost:5000/WeatherForecast");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
