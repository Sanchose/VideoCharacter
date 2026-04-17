namespace VideoCharacter.Controllers;
using VideoCharacter.Models;
public interface IVideoGameCharacterService
{
    Task<List<Character>> GetAllCharactersAsync();
    Task<Character?> GetCharacterByIdAsync(int id);
    Task<Character> AddCharacterAsync(Character character);
    Task<bool> UpdateCharacterAsync();
    Task<bool> DeleteCharacterAsync(int id);
}