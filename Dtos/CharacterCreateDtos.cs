using System.ComponentModel.DataAnnotations;
namespace VideoCharacter.Dtos;
public class CharacterCreateDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string Name { get; set; } = default!;
    [Required(ErrorMessage = "Game is required.")]
    public string Game { get; set; } = default!;
    [Required(ErrorMessage = "Role is required.")]
    public string Role { get; set; } = default!;
}