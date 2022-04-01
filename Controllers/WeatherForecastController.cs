using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreBackendExploratory.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherCacheService _weatherCacheService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherCacheService weatherCacheService)
        {
            this._logger = logger;
            this._weatherCacheService = weatherCacheService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return this._weatherCacheService.GetWeatherForecastSnapshot();
        }
    }
}