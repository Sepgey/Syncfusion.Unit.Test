using System;
using System.Collections.Generic;
using System.Linq;
using Example.DataAccess;
using Example.DataRepository;

namespace Example.BusinessLogic
{
    public class ForecastService
    {
        private IEntityRepository<WeatherForecast> _forecastRepository { get; set; }
        public ForecastService(IEntityRepository<WeatherForecast> forecastRepos)
        {
            _forecastRepository = forecastRepos;
        }

        public List<WeatherForecast> GetActiveForecasts()
        {
            var result = new List<WeatherForecast>();

            result = _forecastRepository.GetAllQueryable().Where(s => s.Date >= new System.DateTime(2021, 12, 27)).ToList();

            return result;
        }

        public bool GetActiveCustomers(WeatherForecast weatherForecast)
        {
            var isAdded = false;
            try
            {
                _forecastRepository.Insert(weatherForecast);
                isAdded = true;
            }
            catch (Exception ex)
            {
                isAdded = false;
            }

            return isAdded;
        }
    }
}