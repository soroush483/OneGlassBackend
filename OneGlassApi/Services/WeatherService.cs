using OneGlassApi.Interfaces;
using OneGlassApi.Models;
using System.Linq;
using System.Net.Http.Headers;

namespace OneGlassApi.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(ILogger<WeatherService> logger)
        {
            _logger = logger;
        }

        public async Task<List<OneGlassWeather>> GetWeatherFromVisualcrossing(string location, string startDate, string endDate)
        {
            try
            {
                using var client = new HttpClient();
                //Create Http Client
                client.BaseAddress = new Uri("https://weather.visualcrossing.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync($"VisualCrossingWebServices/rest/services/timeline/{location}/{startDate}/{endDate}?unitGroup=metric&key=4U5RLT74UKXB5A89GPPZT8YCY");
                if (response.IsSuccessStatusCode)
                {
                    var weatherForecast = await response.Content.ReadAsAsync<Weather>();
                    var results = (from weather in weatherForecast.days select new OneGlassWeather { dateTime = weather.datetime, temperature = weather.temp}).ToList();
                    return results;
                }
                return new List<OneGlassWeather>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<OneGlassWeather>();
            }

        }
    }
}
