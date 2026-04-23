using Microsoft.EntityFrameworkCore;
using VideoCharacter.Models;
namespace VideoCharacter.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> Users => Set<User>();
    }
}