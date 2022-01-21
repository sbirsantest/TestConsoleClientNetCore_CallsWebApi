using System.Threading.Tasks;

namespace ClientNetCoreConsole
{
    public interface ITestPrinter
    {
        Task<string> GetWeatherForecastAsync();
    }
}