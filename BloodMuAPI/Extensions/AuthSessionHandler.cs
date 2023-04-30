using BloodMuAPI.Services.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BloodMuAPI.Extensions
{
    public class AuthSessionHandler : IActionFilter
    {
        private readonly IAccountService _accountService;
        public AuthSessionHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // our code before action executes
            Console.WriteLine("111111");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
            Console.WriteLine("222222");
        }
    }
}
