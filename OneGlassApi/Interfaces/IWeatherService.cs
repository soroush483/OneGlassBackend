using OneGlassApi.Models;

namespace OneGlassApi.Interfaces
{
    public interface IWeatherService
    {
        Task<List<OneGlassWeather>> GetWeatherFromVisualcrossing(string location, string startDate, string endDate);
    }
}
