using BloodMuAPI.DataModel.Data;
using BloodMuAPI.Services.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BloodMuAPI.Extensions
{
    public class AuthSessionHandler : IActionFilter
    {
        private readonly IAccountService _accountService;
        private readonly ISessionManager _sessionManager;
        private readonly ILogger<AuthSessionHandler> _logger;

        /// <summary>
        /// AuthSessionHandler
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="sessionManager"></param>
        /// <param name="logger"></param>
        public AuthSessionHandler(IAccountService accountService, ISessionManager sessionManager, ILogger<AuthSessionHandler> logger)
        {
            _accountService = accountService;
            _sessionManager = sessionManager;
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("sessionId", out object sessionId);
            if (sessionId is string)
            {
               var account = context.HttpContext.Session.Get<AccountSession>(sessionId.ToString());
                if (account is not null)
                {
                    _sessionManager.SetSessionUser(account);
                    _logger.LogInformation($"User got session: " + sessionId.ToString());
                    return;
                }
                _logger.LogError($"No session for " + sessionId.ToString());
                context.Result = new UnauthorizedResult();
                return;
            }

            context.Result = new BadRequestResult();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
