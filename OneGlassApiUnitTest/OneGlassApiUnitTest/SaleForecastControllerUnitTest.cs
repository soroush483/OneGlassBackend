using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using OneGlassApi.Controllers;
using OneGlassApi.Interfaces;
using OneGlassApi.Models;

namespace OneGlassApiUnitTest
{
    [TestClass]
    public class SaleForecastControllerUnitTest
    {

        private readonly Mock<ISalesForecast> _mockSalesForecast;
        private readonly ILogger<SaleForecastController> _logger = new NullLogger<SaleForecastController>();
        private readonly SaleForecastController _controller;
        public SaleForecastControllerUnitTest()
        {
            _mockSalesForecast = new Mock<ISalesForecast>();
            _controller = new SaleForecastController(_logger, _mockSalesForecast.Object);
        }

        [TestMethod]
        public async Task GetSaleForcast_ReturnsSaleForcast()
        {
            // Arrange
            var expectedSaleForcast = new List<OneGlassSale> {
                new OneGlassSale
                {
                    location = "Hamburg",
                    saleDate = DateTime.Parse("2023-02-03T00:00:00"),
                    forecastedSalesQuantity = 239.75876370433235
                }
            };
            _mockSalesForecast.Setup(x => x.GetSalesForecast("Hamburg", "03-03-2023", "03-03-2023")).Returns(expectedSaleForcast);

            // Act
            var actionResult =  _controller.Get("Hamburg", "03-03-2023","03-03-2023") as OkObjectResult;


            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<OneGlassSale>));
        }
    }
}
