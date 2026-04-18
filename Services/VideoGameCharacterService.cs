using VideoCharacter.Models;
using VideoCharacter.Controllers;
using VideoCharacter.Data;
using Microsoft.EntityFrameworkCore;
namespace VideoCharacter.Controllers;

public class VideoGameCharacterService(AppDbContext _context) : IVideoGameCharacterService
{
    public async Task<List<Character>> GetAllCharactersAsync()
    {
        return await _context.Characters.ToListAsync();
    }

    public async Task<Character?> GetCharacterByIdAsync(int id)
    {
        var character = await _context.Characters.FindAsync(id);
        return character;
    }

    public Task<Character> AddCharacterAsync(Character character)
    {
        var entry = _context.Characters.Add(character);
        _context.SaveChanges();
        return Task.FromResult(entry.Entity);
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