using Microsoft.AspNetCore.Mvc;
using OneGlassApi.Interfaces;
using OneGlassApi.Models;

namespace OneGlassApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISalesForecast _salesForecast;
        public SaleForecastController(ILogger<WeatherForecastController> logger, ISalesForecast salesForecast)
        {
            _logger = logger;
            _salesForecast = salesForecast;
        }

        [HttpGet(Name = "GetSalesForecast")]
        public List<OneGlassSale> Get(string location, string startDate, string endDate)
        {
            return _salesForecast.GetSalesForecast(location, startDate, endDate);
        }
    }
}
