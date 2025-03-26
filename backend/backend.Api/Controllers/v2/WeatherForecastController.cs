using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/weatherforecast")]
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetV2()
        {
            return Ok(new
            {
                Version = "v2",
                Data = Enumerable.Range(1, 5).Select(index => new
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
            });
        }
    }
}
