namespace VideoCharacter.Controllers;
using VideoCharacter.Models;
using VideoCharacter.Dtos;
public interface IVideoGameCharacterService
{
    Task<List<CharacterResponse>> GetAllCharactersAsync();
    Task<CharacterResponse?> GetCharacterByIdAsync(int id);
    Task<Character> AddCharacterAsync(Character character);
    Task<Character> AddCharacterAsync(CharacterCreateDto characterDto);
    Task<bool> UpdateCharacterAsync(int id, Character character);
    Task<bool> DeleteCharacterAsync(int id);
}