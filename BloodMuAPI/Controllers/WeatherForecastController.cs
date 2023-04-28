using Microsoft.AspNetCore.Mvc;

namespace BloodMuAPI.Controllers
{
    [ApiController]
    [Route("v1/test")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [Route("Get")]
        [HttpGet]
        public IActionResult Get()
        {
            Console.WriteLine("AAAAAAA!!!!");
            return Ok("Aaaaa!");
        }
    }
}