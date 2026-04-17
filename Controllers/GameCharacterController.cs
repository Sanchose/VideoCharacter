using Microsoft.AspNetCore.Mvc;
using VideoCharacter.Models;

namespace VideoCharacter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameCharacterController(IVideoGameCharacterService service) : ControllerBase
{
        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetCharacters()
        {
            return Ok(await service.GetAllCharactersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
            var character = await service.GetCharacterByIdAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);  
        }

    }
}