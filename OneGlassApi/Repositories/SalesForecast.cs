using Microsoft.Extensions.Options;
using Npgsql;
using OneGlassApi.Interfaces;
using OneGlassApi.Models;
using OneGlassApi.Options;

namespace OneGlassApi.Repositories
{
    public class SalesForecast : ISalesForecast
    {
        private readonly ILogger<SalesForecast> _logger;
        private readonly DatabaseOption _databaseOption;

        public SalesForecast(ILogger<SalesForecast> logger, IOptions<DatabaseOption> databaseOption)
        {
            _logger = logger;
            _databaseOption = databaseOption.Value;
        }
        public List<OneGlassSale> GetSalesForecast(string location, string startDate, string endDate)
        {
            try
            {
                var results = new List<OneGlassSale>();
                using var conn = new NpgsqlConnection(_databaseOption.ConnectionString);
                conn.Open();
                using var command = new NpgsqlCommand($"SELECT * FROM oneglass.forecasts WHERE location = '{location}' AND date BETWEEN '{startDate}' AND '{endDate}'", conn);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new OneGlassSale
                    {
                        saleDate = reader.GetDateTime(0),
                        location = reader.GetString(1),
                        forecastedSalesQuantity = reader.GetDouble(2)
                    });
                }
                reader.Close();
                return results;
            }
            catch (Exception e)
            {
                _logger.LogError("Query to database failed with exeption {exeption}", e.ToString());
                throw;
            }

        }
    }
}
