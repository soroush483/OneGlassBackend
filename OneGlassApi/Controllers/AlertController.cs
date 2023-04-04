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
        public async Task<IActionResult> GetAlertForCloseDays(string location, string startDate, string endDate)
        {
            try
            {
                var result = await _alertService.ShowClosedDaysAlert(location, startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get Alerts with exception {exception}", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
