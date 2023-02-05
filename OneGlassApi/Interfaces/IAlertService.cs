using OneGlassApi.Models;

namespace OneGlassApi.Interfaces
{
    public interface IAlertService
    {
        Task<List<OneGlassCloseDays>> ShowClosedDaysAlert(string location, string startDate, string endDate);
    }
}
