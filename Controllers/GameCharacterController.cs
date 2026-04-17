using Microsoft.AspNetCore.Mvc;
using VideoCharacter.Models;

namespace VideoCharacter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameCharacterController : ControllerBase
    {
        static List<Character> Characters = new List<Character>
        {
            new Character { Id = 1, Name = "Mario", Game = "Super Mario Bros.", Role = "Protagonist" },
            new Character { Id = 2, Name = "Link", Game = "The Legend of Zelda", Role = "Protagonist" },
            new Character { Id = 3, Name = "Master Chief", Game = "Halo", Role = "Protagonist" },
        };

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetCharacters()
        {
            return await Task.FromResult(Characters);
        }

    }
}