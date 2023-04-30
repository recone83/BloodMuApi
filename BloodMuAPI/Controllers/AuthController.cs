
using BloodMuAPI.DataProvider;
using BloodMuAPI.Extensions;
using BloodMuAPI.Services.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodMuAPI.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login([FromServices] IAccountService db)
        {
            return Ok(db.GetUsers());
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            return Ok("logout");
        }
    }
}