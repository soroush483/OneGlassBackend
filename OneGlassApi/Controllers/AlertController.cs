using Microsoft.AspNetCore.Mvc;
using OneGlassApi.Interfaces;
using OneGlassApi.Models;

namespace OneGlassApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlertController : ControllerBase
    {
        private readonly ILogger<AlertController> _logger;
        private readonly IAlertService _alertService;
        public AlertController(ILogger<AlertController> logger, IAlertService alertService)
        {
            _logger = logger;
            _alertService = alertService;
        }
        [HttpGet(Name = "GetAlerts")]
        public Task<List<OneGlassCloseDays>> GetAlertForCloseDays(string location, string startDate, string endDate)
        {
            return _alertService.ShowClosedDaysAlert(location, startDate, endDate);
        }
    }
}
