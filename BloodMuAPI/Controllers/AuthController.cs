
using BloodMuAPI.DataModel.Data;
using BloodMuAPI.Extensions;
using BloodMuAPI.Services.API;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BloodMuAPI.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAccountService _db;
        public AuthController(ILogger<AuthController> logger, IAccountService db)
        {
            _logger = logger;
            _db = db;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([Required]string username, [Required]string password)
        {
            var account = _db.GetUser(username, password);
            if(account is not null)
            {
                var sessionId = Guid.NewGuid().ToString();
                HttpContext.Session.Set<AccountSession>(sessionId, account.GetAccountForSession());

                return Ok(sessionId);
            }
            return Ok(null);
        }

        [Route("test2")]
        [HttpGet]
        [ServiceFilter(typeof(AuthSessionHandler))]
        public IActionResult Test2([FromHeader] string sessionId)
        {
            return Ok(HttpContext.Session.Get<AccountSession>(sessionId));
        }
    }
}