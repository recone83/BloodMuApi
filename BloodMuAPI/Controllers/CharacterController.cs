using BloodMuAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BloodMuAPI.Controllers
{
    [ApiController]
    [Route("v1/character")]
    public class CharacterController : Controller
    {
        [Route("get")]
        [HttpGet]
        [ServiceFilter(typeof(AuthSessionHandler))]
        public IActionResult Get([FromHeader] string sessionId,string nameCharacter)
        {
            return Ok();
        }

        [Route("getByAccountName")]
        [HttpGet]
        [ServiceFilter(typeof(AuthSessionHandler))]
        public IActionResult GetByAccountName([FromHeader] string sessionId, string nameCharacter)
        {
            return Ok();
        }
    }
}
