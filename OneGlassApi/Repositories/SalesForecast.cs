using Npgsql;
using OneGlassApi.Interfaces;
using OneGlassApi.Models;

namespace OneGlassApi.Repositories
{
    public class SalesForecast : ISalesForecast
    {
        private readonly ILogger<SalesForecast> _logger;
        private static string Host = "voids-jobs.c2wwnfcaisej.eu-central-1.rds.amazonaws.com";
        private static string User = "postgres_ro";
        private static string DBname = "postgres";
        private static string Password = "0123456789";
        private static string Port = "5432";

        public SalesForecast(ILogger<SalesForecast> logger)
        {
            _logger= logger;
        }
        public List<OneGlassSale> GetSalesForecast(string location, string startDate, string endDate)
        {
            var results = new List<OneGlassSale>();
            // Build connection string using parameters from portal
            string connString =
                String.Format(
                    "Server={0}; User Id={1}; Database={2}; Port={3}; Password={4};SSLMode=Prefer",
                    Host,
                    User,
                    DBname,
                    Port,
                    Password);
            using var conn = new NpgsqlConnection(connString);
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
    }
}
