using System.ComponentModel.DataAnnotations;
using VideoCharacter.Models;
using VideoCharacter.Controllers;
namespace VideoCharacter.Dtos;
public class CharacterCreateDto
{
    public enum CharacterRole
    {
        // CS2
        Protagonist,
        Antagonist,
        Supporting,

        // Rust
        Dachlose,
        Toproof,
        BigBoss
    }
    public enum CharacterGame
    {
        CS2,
        Rust
    }
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string Name { get; set; } = default!;
    [Required(ErrorMessage = "Game is required.")]
    [EnumDataType(typeof(CharacterGame), ErrorMessage = "Game must be a valid enum value.")]
    public CharacterGame? Game { get; set; } = default!;
    [Required(ErrorMessage = "Role is required.")]
    [EnumDataType(typeof(CharacterRole), ErrorMessage = "Role must be a valid enum value.")]
    public CharacterRole Role { get; set; } = default!;

}