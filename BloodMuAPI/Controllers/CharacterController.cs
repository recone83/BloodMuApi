using BloodMuAPI.DataModel.Data;
using BloodMuAPI.Extensions;
using BloodMuAPI.Services.API;
using Microsoft.AspNetCore.Mvc;

namespace BloodMuAPI.Controllers
{
    [ApiController]
    [Route("v1/character")]
    public class CharacterController : Controller
    {
        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] ICharacterService service, string nameCharacter)
        {
            var row = await service.GeCharacter(nameCharacter);
            return Json(new
            {
                Name = row.Name,
                Class = row.CharacterClass?.Name,
                CurrentMap =  row.CurrentMap?.Name,
                X = row.PositionX,
                Y = row.PositionY,
                Exp = row.Experience,
                LVL = row.Attributes.First(x => x.Definition.Designation == "Level")?.Value,
                Reset = row.Attributes.First(x => x.Definition.Designation == "Resets")?.Value
            });
        }

        [Route("ranking/resets")]
        [HttpGet]
        public async Task<IActionResult> GetRankingReset([FromServices] ICharacterService service)
        {
            var rows = await service.GeResets()!;
            return Ok(rows);
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
