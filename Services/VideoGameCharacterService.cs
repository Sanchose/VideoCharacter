using VideoCharacter.Models;
using VideoCharacter.Controllers;
namespace VideoCharacter.Controllers;

public class VideoGameCharacterService : IVideoGameCharacterService
{
    static List<Character> Characters = new List<Character>
        {
            new Character { Id = 1, Name = "Mario", Game = "Super Mario Bros.", Role = "Protagonist" },
            new Character { Id = 2, Name = "Link", Game = "The Legend of Zelda", Role = "Protagonist" },
            new Character { Id = 3, Name = "Master Chief", Game = "Halo", Role = "Protagonist" },
            new Character { Id = 4, Name = "dfvdfvdfv", Game = "dfvdfvdv", Role = "dfvdfvdf" },
        };
    public Task<List<Character>> GetAllCharactersAsync()
    {
        return Task.FromResult(Characters);
    }

    public async Task<Character?> GetCharacterByIdAsync(int id)
    {
        var character = Characters.FirstOrDefault(c => c.Id == id);
        return await Task.FromResult(character);
    }

    public Task<Character> AddCharacterAsync(Character character)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCharacterAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCharacterAsync(int id)
    {
        throw new NotImplementedException();
    }

}