using BloodMuAPI.DataModel.Data;
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
        public async Task<IActionResult> Get([FromHeader] string sessionId, [FromServices] ISessionManager sessionManager)
        {
            var user = sessionManager.GetSessionUser();
            var account = await _accountService.GetUser(user.LoginName);

            return Ok(account);
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromServices] IAccountService service, AccountPost payload)
        {
            if (await service.AddAccount(payload))
                return Ok();
            else
                return BadRequest();
        }
    }
}
