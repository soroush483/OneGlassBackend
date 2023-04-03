using OneGlassApi.Interfaces;
using OneGlassApi.Models;

namespace OneGlassApi.Services
{
    public class AlertService : IAlertService
    {
        private readonly IWeatherService _weatherService;
        private readonly ISalesForecast _salesForecast;
        public AlertService(IWeatherService weatherService, ISalesForecast salesForecast)
        {
            _weatherService = weatherService;
            _salesForecast = salesForecast;
        }
        //Get range of days and return a list of colse or unclosed days
        public async Task<List<OneGlassCloseDays>> ShowClosedDaysAlert(string location, string startDate, string endDate)
        {
            //Get sales data from Db
            var salesData = _salesForecast.GetSalesForecast(location, startDate, endDate);
            var closeBySaleDates = CloseByLowSale(salesData);
            //Get weather data from Externl Api
            var weatherData = await _weatherService.GetWeatherFromVisualcrossing(location, startDate, endDate);
            var closeByBadWeatherDates = CloseByBadWeather(weatherData, salesData, closeBySaleDates);
            //return
            return closeBySaleDates.Union(closeByBadWeatherDates).ToList();
        }
        //Close by low sale
        private List<OneGlassCloseDays> CloseByLowSale(List<OneGlassSale> oneGlassSales)
        {
            List<OneGlassCloseDays> closedDays = new();
            for (int i = 0; i < oneGlassSales.Count - 2; i++)
            {

                if (oneGlassSales[i].forecastedSalesQuantity + oneGlassSales[i + 1].forecastedSalesQuantity + oneGlassSales[i + 2].forecastedSalesQuantity < 3000)
                {
                    if (i >= 3 && !(closedDays[closedDays.Count - 1].isClosed && closedDays[closedDays.Count - 2].isClosed && closedDays[closedDays.Count - 3].isClosed))
                    {
                        closedDays.Add(new OneGlassCloseDays { location = oneGlassSales[i].location, saleDate = oneGlassSales[i].saleDate, forecastedSalesQuantity = oneGlassSales[i].forecastedSalesQuantity, isClosed = true, reason = "LowSale" });
                    }
                    else if (i < 3)
                    {
                        closedDays.Add(new OneGlassCloseDays { location = oneGlassSales[i].location, saleDate = oneGlassSales[i].saleDate, forecastedSalesQuantity = oneGlassSales[i].forecastedSalesQuantity, isClosed = true, reason = "LowSale" });
                    }
                    else
                    {
                        closedDays.Add(new OneGlassCloseDays { location = oneGlassSales[i].location, saleDate = oneGlassSales[i].saleDate, forecastedSalesQuantity = oneGlassSales[i].forecastedSalesQuantity, isClosed = false, reason = "" });
                    }
                }
            }
            //leave the last two days of requested interval open because we can not verify thier sales for the next coming 3 days due to lack of data
            closedDays.Add(new OneGlassCloseDays { location = oneGlassSales[oneGlassSales.Count - 2].location, saleDate = oneGlassSales[oneGlassSales.Count - 2].saleDate, forecastedSalesQuantity = oneGlassSales[oneGlassSales.Count - 2].forecastedSalesQuantity, isClosed = false, reason = "" });
            closedDays.Add(new OneGlassCloseDays { location = oneGlassSales[oneGlassSales.Count - 1].location, saleDate = oneGlassSales[oneGlassSales.Count - 1].saleDate, forecastedSalesQuantity = oneGlassSales[oneGlassSales.Count - 1].forecastedSalesQuantity, isClosed = false, reason = "" });

            return closedDays;
        }

        //Close by low temprature
        private List<OneGlassCloseDays> CloseByBadWeather(List<OneGlassWeather> oneGlassWeathers, List<OneGlassSale> oneGlassSales, List<OneGlassCloseDays> closedDays)
        {
            for (int i = 0; i < oneGlassSales.Count - 2; i++)
            {
                if (oneGlassWeathers[i].temperature + oneGlassWeathers[i + 1].temperature + oneGlassWeathers[i + 2].temperature < 15)
                {
                    if (oneGlassSales[i].forecastedSalesQuantity + oneGlassSales[i + 1].forecastedSalesQuantity + oneGlassSales[i + 2].forecastedSalesQuantity < 4500)
                    {
                        if (i >= 3 && !(closedDays[closedDays.Count - 1].isClosed && closedDays[closedDays.Count - 2].isClosed && closedDays[closedDays.Count - 3].isClosed))
                        {
                            if (!(closedDays.Exists(x => x.saleDate == oneGlassSales[i].saleDate)))
                            {
                                closedDays.Add(new OneGlassCloseDays { location = oneGlassSales[i].location, saleDate = oneGlassSales[i].saleDate, forecastedSalesQuantity = oneGlassSales[i].forecastedSalesQuantity, isClosed = true, temperature = oneGlassWeathers[i].temperature, reason = "BadWeather" });
                            }
                        }
                        if (i < 3)
                        {
                            if (!(closedDays.Exists(x => x.saleDate == oneGlassSales[i].saleDate)))
                            {
                                closedDays.Add(new OneGlassCloseDays { location = oneGlassSales[i].location, saleDate = oneGlassSales[i].saleDate, forecastedSalesQuantity = oneGlassSales[i].forecastedSalesQuantity, isClosed = true, temperature = oneGlassWeathers[i].temperature, reason = "BadWeather" });
                            }
                        }
                    }
                }
            }
            return closedDays;
        }
    }
}
