using Microsoft.AspNetCore.Mvc;

namespace BloodMuAPI.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login()
        {
            return Ok("login");
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            return Ok("logout");
        }
    }
}