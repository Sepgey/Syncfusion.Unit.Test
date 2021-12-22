using System.Collections.Generic;
using System.Linq;
using Example.BusinessLogic;
using Example.DataAccess;
using Example.DataRepository;
using Moq;
using NUnit.Framework;

namespace Example.Tests.TestMethods
{
    public class ForecastControllerBL
    {
        private Mock<IEntityRepository<WeatherForecast>> forecastRepository;
        private List<WeatherForecast> forecastsList;
        [SetUp]
        public void Setup()
        {
            //Set up the mock
            forecastRepository = new Mock<IEntityRepository<WeatherForecast>>();
            forecastsList = new List<WeatherForecast>();
            forecastsList.Add(new WeatherForecast() { Date = new System.DateTime(2021, 12, 22), TemperatureC = -15, Summary = "Bracing" });
            forecastsList.Add(new WeatherForecast() { Date = new System.DateTime(2021, 12, 23), TemperatureC = -20, Summary = "Freezing" });
            forecastsList.Add(new WeatherForecast() { Date = new System.DateTime(2021, 11, 24), TemperatureC = -5, Summary = "Cold" });
        }

        [Test]
        public void TestGetActiveRecords()
        {
            //Act
            forecastRepository.Setup(a => a.GetAllQueryable()).Returns(forecastsList.AsQueryable());

            //Arrange
            var foreacstService = new ForecastService(forecastRepository.Object);
            var forecastList = foreacstService.GetActiveForecasts();

            //Arrest
            Assert.IsTrue(forecastList.All(s => s.TemperatureC <= -15));
            Assert.IsTrue(forecastList.All(s => s.Date >= new System.DateTime(2021, 12, 21)));
        }
    }
}