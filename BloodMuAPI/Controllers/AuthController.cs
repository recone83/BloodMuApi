
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
        public async Task<IActionResult> Login([Required] Login data)
        {
            var account = await _db.GetUser(data);
            if (account is not null)
            {
                var sessionId = Guid.NewGuid().ToString();
                HttpContext.Session.Set<AccountSession>(sessionId, account.GetAccountForSession());

                return Ok(sessionId);
            }
            return StatusCode(401);
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

        private IEnumerable<string>? ReadLastLines(string filePath, int numberOfLines)
        {
            IEnumerable<string> line;
            if (System.IO.File.Exists(filePath))
            {
                _logger.LogError("Istnieje....");
                return System.IO.File.ReadLines(filePath).ToList();
            }
            _logger.LogError("nie istnieje");
            return null;
        }

        /// <summary>
        /// Read chat logs
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [Route("chat/log")]
        [HttpGet]
        public async Task<IActionResult> GetGlobalChatLog([FromServices] ICharacterService service)
        {
            var numberOfLines = 50; 

            _logger.LogError("Start");
            _logger.LogError("Start :" + _config["ChatTextFile"]);
   
            var lastLines = ReadLastLines(_config["ChatTextFile"], numberOfLines);
            _logger.LogError("end :" + lastLines.ToArray());

            return View(lastLines);
        }
    }
}