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
            var testPrinter = ActivatorUtilities.CreateInstance<TestPrinter>(host.Services);
            Console.WriteLine(await testPrinter.GetWeatherForecastAsync());
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) => 
            {
                services.AddHttpClient();
                services.AddTransient<ITestPrinter, TestPrinter>();
            });
    }
}
