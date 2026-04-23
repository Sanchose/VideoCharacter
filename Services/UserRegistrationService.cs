using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VideoCharacter.Data;
using VideoCharacter.Dtos;
using VideoCharacter.Exceptions;
using VideoCharacter.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using BCrypt.Net;
public class RegistrationService
{
    private readonly AppDbContext appDbContext;
    private readonly string _key = "THIS_IS_SUPER_SECRET_KEY_1234567890";

    public RegistrationService(AppDbContext _appDbContext)
    {
        this.appDbContext = _appDbContext;
    }

    public AuthResponseDto Register(UserRegisterDto registerDto)
    {
        var user = appDbContext.Users.FirstOrDefault(u => u.Username == registerDto.Username);
        if (user != null)
        {
            throw new InvalidOperationException("Username already exists");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        var newUser = new User
        {
            Username = registerDto.Username,
            PasswordHash = passwordHash
        };
        appDbContext.Users.Add(newUser);
        appDbContext.SaveChanges();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, newUser.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new AuthResponseDto { Token = tokenHandler.WriteToken(token) };
    }

}