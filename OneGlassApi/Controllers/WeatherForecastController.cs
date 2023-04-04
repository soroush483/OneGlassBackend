using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneGlassApi.Interfaces;
using OneGlassApi.Models;

namespace OneGlassApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {      
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet(Name = "GetWeatherForecast"), Authorize]
        public async Task<IActionResult> Get(string location, string startDate, string endDate)
        {
            try
            {
                var result = await _weatherService.GetWeatherFromVisualcrossing(location, startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get weather forecast beacuase exception {exception}", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}