using System.Globalization;
using FluentValidation;
using VideoCharacter.Dtos;
using VideoCharacter.Models;

public class CharacterCreateDtoValidator : AbstractValidator<CharacterCreateDto>
{
    public CharacterCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.")
            .Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("Name can only contain letters, numbers, and spaces.");

        RuleFor(x => x.Game)
        .NotNull().WithMessage("Game is required")
        .IsInEnum().WithMessage("Invalid game");

        RuleFor(x => x)
        .Must(BeValidRoleForGame)
        .WithMessage("Invalid role for the specified game");
    }
    private bool BeValidRoleForGame(CharacterCreateDto dto)
    {
        if (dto.Game == CharacterCreateDto.CharacterGame.Rust)
        {
            return dto.Role == CharacterCreateDto.CharacterRole.Dachlose ||
                   dto.Role == CharacterCreateDto.CharacterRole.Toproof ||
                   dto.Role == CharacterCreateDto.CharacterRole.BigBoss;
        }

        if (dto.Game == CharacterCreateDto.CharacterGame.CS2)
        {
            return dto.Role == CharacterCreateDto.CharacterRole.Protagonist ||
                   dto.Role == CharacterCreateDto.CharacterRole.Antagonist ||
                   dto.Role == CharacterCreateDto.CharacterRole.Supporting;
        }

        return false;
    }
}