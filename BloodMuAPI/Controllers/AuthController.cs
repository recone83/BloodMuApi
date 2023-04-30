using BloodMuAPI.Data;
using BloodMuAPI.DataModel.Entities;
using BloodMuAPI.Extensions;
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
        public IActionResult Login([FromServices] BloodMuDbContext db)
        {
            var x = db.Accounts
                .Include(c => c.Characters)
                    .ThenInclude(c => c.Inventory)
                        .ThenInclude(c => c.Items)
                            .ThenInclude(c => c.Definition)
                .Include(c => c.Characters)
                    .ThenInclude(c => c.Attributes)
                        .ThenInclude(c => c.Definition)
                .Include(c => c.Characters)
                    .ThenInclude(c => c.CharacterClass)

                .First();

            return Ok(x);
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            return Ok("logout");
        }
    }
}