using Microsoft.AspNetCore.Mvc;
using VideoCharacter.Dtos;
using VideoCharacter.Models;

namespace VideoCharacter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameCharacterController(IVideoGameCharacterService service) : ControllerBase
{
        [HttpGet]
        public async Task<ActionResult<List<CharacterResponse>>> GetCharacters()
        {
            return Ok(await service.GetAllCharactersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterResponse>> GetCharacter(int id)
        {
            var character = await service.GetCharacterByIdAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);  
        }
        [HttpPost]
        public async Task<ActionResult<Character>> AddCharacter([FromBody] Character character)
        {
            var addedCharacter = await service.AddCharacterAsync(character);
            return CreatedAtAction(nameof(GetCharacter), new { id = addedCharacter.Id }, addedCharacter);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            var success = await service.DeleteCharacterAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}