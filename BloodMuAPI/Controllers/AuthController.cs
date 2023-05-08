
using BloodMuAPI.DataModel.Data;
using BloodMuAPI.Extensions;
using BloodMuAPI.Services.API;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BloodMuAPI.Controllers
{

    public class ServerStatusModel
    {
        public string state { get; set; }
        public int players { get; set; }
        public List<string> playersList { get; set; }
    }

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
        public IActionResult Login([Required] string username, [Required] string password)
        {
            var account = _db.GetUser(username, password);
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
        public async Task<IActionResult> GetStatus()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config["OpenMuAdminPanel"]);

            var response = await httpClient.GetAsync("/api/status");
            response.EnsureSuccessStatusCode();
            return Json(response.Content.ReadFromJsonAsync<ServerStatusModel>().Result);
        }
    }
}