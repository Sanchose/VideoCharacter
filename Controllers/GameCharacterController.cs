using Microsoft.AspNetCore.Mvc;
using VideoCharacter.Dtos;
using VideoCharacter.Models;
using VideoCharacter.Data;
using Microsoft.EntityFrameworkCore;

namespace VideoCharacter.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]

    public class GameCharacterController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IVideoGameCharacterService _service;

        public GameCharacterController(
            AppDbContext context,
            IVideoGameCharacterService service)
        {
            _context = context;
            _service = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<CharacterResponse>>> GetCharacters()
        {
            return Ok(await _service.GetAllCharactersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterResponse>> GetCharacter(int id)
        {
            var character = await _service.GetCharacterByIdAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);  
        }
        [HttpGet("search")]
        public async Task<ActionResult<List<CharacterResponse>>> GetCharacter(string? role, string? game)
        {
            
            if (string.IsNullOrEmpty(role) && string.IsNullOrEmpty(game))
                return BadRequest("Вкажи role або game");

            var query = _context.Characters.AsQueryable();

            query = query.Where(x =>
                (!string.IsNullOrEmpty(role) && x.Role == role) ||
                (!string.IsNullOrEmpty(game) && x.Game == game)
            );

            var result = await query.ToListAsync();

            if (!result.Any())
                return NotFound("Character not found");

            return Ok(result);
        }
        [HttpPost("Full")]
        public async Task<ActionResult<Character>> AddCharacter([FromBody] Character character)
        {
            var addedCharacter = await _service.AddCharacterAsync(character);
            return CreatedAtAction(nameof(GetCharacter), new { id = addedCharacter.Id }, addedCharacter);
        }
        [HttpPost]
        public async Task<ActionResult<Character>> AddCharacter([FromBody] CharacterCreateDto dto)
        {
            var character = new Character
            {
                Name = dto.Name,
                Game = dto.Game,
                Role = dto.Role.ToString()
            };

            var addedCharacter = await _service.AddCharacterAsync(character);

            return CreatedAtAction(nameof(GetCharacter), new { id = addedCharacter.Id }, addedCharacter);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            var success = await _service.DeleteCharacterAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCharacter(int id, [FromBody] Character character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }
            var success = await _service.UpdateCharacterAsync(id, character);
            if (!success)            {
                return NotFound(); }
            return NoContent();
        }
}
}