using VideoCharacter.Models;
using VideoCharacter.Controllers;
using VideoCharacter.Data;
using Microsoft.EntityFrameworkCore;
using VideoCharacter.Dtos;
namespace VideoCharacter.Controllers;

public class VideoGameCharacterService(AppDbContext _context) : IVideoGameCharacterService
{
    public async Task<List<CharacterResponse>> GetAllCharactersAsync()
    {
        return await _context.Characters.Select(c => new CharacterResponse
        {
            Name = c.Name,
            Game = c.Game,
            Role = c.Role
        }).ToListAsync();

    }

    public async Task<CharacterResponse?> GetCharacterByIdAsync(int id)
    {
        return await _context.Characters.Where(c => c.Id == id).Select(c => new CharacterResponse
        {
            Name = c.Name,
            Game = c.Game,
            Role = c.Role
        }).FirstOrDefaultAsync();
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
        var character = _context.Characters.Find(id);
        if (character == null)
        {
            return Task.FromResult(false);
        }
        _context.Characters.Remove(character);
        _context.SaveChanges();
        return Task.FromResult(true);
    }

}