using System.ComponentModel.DataAnnotations;

public class UserLoginDto
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}