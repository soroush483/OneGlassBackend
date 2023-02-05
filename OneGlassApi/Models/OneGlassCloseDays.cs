using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OneGlassApi.Models
{
    public class OneGlassCloseDays
    {
        public string location { get; set; }
        public DateTime saleDate { get; set; }
        public double forecastedSalesQuantity { get; set; }
        public double temperature { get; set; }
        public bool isClosed { get; set; }
        public string reason { get; set; }
    }
}
