using System.Collections.Concurrent;

namespace AspnetCoreBackendExploratory
{
    public class WeatherCacheService : IHostedService
    {

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private Timer _timer = null!;



        /// <summary>
        /// Caching for weather data.
        /// </summary>
        private ConcurrentDictionary<DateTime, WeatherForecast> WeatherDataCache = new ConcurrentDictionary<DateTime, WeatherForecast>();


        public Task StartAsync(CancellationToken cancellationToken)
        {
            foreach(WeatherForecast weatherForecast in Enumerable.Range(1, 10).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }))
            {
                this.WeatherDataCache[weatherForecast.Date] = weatherForecast;
            }

            this._timer = new Timer(this.ContinueProcessing, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }

        public void ContinueProcessing(object? state)
        {
            foreach(KeyValuePair<DateTime, WeatherForecast> forecastKVP in this.WeatherDataCache)
            {
                this.WeatherDataCache[forecastKVP.Key].TemperatureC += Random.Shared.Next(-1, 1);
            }
        }


        /// <summary>
        /// Returns an enumerated array for the current cached weather forecasts
        /// </summary>
        /// <returns></returns>
        public WeatherForecast[] GetWeatherForecastSnapshot()
        {
            return this.WeatherDataCache.Values.ToArray();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this._timer?.Dispose();
        }


    }
}
