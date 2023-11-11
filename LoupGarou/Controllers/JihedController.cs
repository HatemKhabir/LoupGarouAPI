using Microsoft.AspNetCore.Mvc;

namespace LoupGarou.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JihedController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<JihedController> _logger;

        public JihedController(ILogger<JihedController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("Team")]
        public IEnumerable<string> GetTeam()
        {
            var teamList = new List<string>()
            {
                "Mehdi", "Hatem", "Njoura", "Jihed"
            };
            return teamList.ToArray();
        }
    }
}