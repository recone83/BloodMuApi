
using BloodMuAPI.DataProvider;
using BloodMuAPI.Extensions;
using BloodMuAPI.Services.API;
using Microsoft.AspNetCore.Authorization;
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

        [Route("test1")]
        [HttpGet]
        [ServiceFilter(typeof(AuthSessionHandler))]
        public IActionResult Test1()
        {
            HttpContext.Session.SetString("test1", "AAAAAA");
            Console.WriteLine("Test1");
            return Ok("Test1");
        }

        [Route("test2")]
        [HttpGet]
        public IActionResult Test2()
        {
            Console.WriteLine("Test2");
            return Ok(HttpContext.Session.GetString("test1"));
        }
    }
}