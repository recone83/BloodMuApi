
using BloodMuAPI.DataModel.Data;
using BloodMuAPI.DataModel.Data.Accounts;
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
        private readonly IConfiguration _config;
        public AuthController(ILogger<AuthController> logger, IAccountService db, IConfiguration config)
        {
            _config = config;
            _logger = logger;
            _db = db;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([Required] string username, [Required] string password)
        {
            var account = await _db.GetUser(username, password);
            if (account is not null)
            {
                var sessionId = Guid.NewGuid().ToString();
                HttpContext.Session.Set<AccountSession>(sessionId, account.GetAccountForSession());

                return Ok(sessionId);
            }
            return Ok(null);
        }

        /// <summary>
        /// Get OpenMu server status
        /// </summary>
        /// <returns></returns>
        [Route("status")]
        [HttpGet]
        public async Task<IActionResult> GetStatus([FromServices] ICharacterService service)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config["OpenMuAdminPanel"]);

            var response = await httpClient.GetAsync("/api/status");
            response.EnsureSuccessStatusCode();

            var status = response.Content.ReadFromJsonAsync<SystemStats>().Result;
            var statsFromDb = await service.GetStats();
            status.Accounts = statsFromDb.Accounts;
            status.Characters = statsFromDb.Characters;

            return Json(status);
        }
    }
}