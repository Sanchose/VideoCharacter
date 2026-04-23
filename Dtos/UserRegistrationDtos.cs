using System.ComponentModel.DataAnnotations;

public class UserRegisterDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Username { get; set; } = default!;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = default!;
}