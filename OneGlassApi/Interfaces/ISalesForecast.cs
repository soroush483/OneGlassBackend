using OneGlassApi.Models;

namespace OneGlassApi.Interfaces
{
    public interface ISalesForecast
    {
        List<OneGlassSale> GetSalesForecast(string location, string startDate, string endDate);
    }
}
