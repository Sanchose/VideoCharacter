namespace VideoCharacter.Controllers;
using VideoCharacter.Models;
using VideoCharacter.Dtos;
public interface IVideoGameCharacterService
{
    Task<List<CharacterResponse>> GetAllCharactersAsync();
    Task<CharacterResponse?> GetCharacterByIdAsync(int id);
    Task<Character> AddCharacterAsync(Character character);
    Task<bool> UpdateCharacterAsync();
    Task<bool> DeleteCharacterAsync(int id);
}