using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model;
using NZWalks.API.Services;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet]
        public async Task<IActionResult> GetWeatherAsync()
        {
            // reference null check:
            if (_weatherService == null)
            {
                _logger.LogError("WeatherService is null");
                return StatusCode(500, "An error occurred while processing your request");
            }

            try
            {
                var weatherData = await _weatherService.GetWeatherAsyn();
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get weather data");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpGet("stations")]
        public async Task<IActionResult> GetStationsAsync()
        {
            if (_weatherService == null)
            {
                _logger.LogError("WeatherService is null");
                return StatusCode(500, "An error occurred while processing your request");
            }
            try
            {
                var stations = await _weatherService.GetStationAsync();
                return Ok(stations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get station data");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}
