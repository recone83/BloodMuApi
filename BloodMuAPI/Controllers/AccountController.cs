using BloodMuAPI.Extensions;
using BloodMuAPI.Services.API;
using Microsoft.AspNetCore.Mvc;

namespace BloodMuAPI.Controllers
{
    [ApiController]
    [Route("v1/account")]
    public class AccountController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAccountService _accountService;
        public AccountController(ILogger<AuthController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [Route("get")]
        [HttpGet]
        [ServiceFilter(typeof(AuthSessionHandler))]
        public IActionResult Test1([FromHeader] string sessionId, [FromServices] ISessionManager sessionManager)
        {
            var user = sessionManager.GetSessionUser();
            Console.WriteLine(user.LoginName);
            var account = _accountService.GetUser(user.LoginName);

            return Ok(account);
        }
    }
}
