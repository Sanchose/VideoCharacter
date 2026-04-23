using FluentValidation;
using VideoCharacter.Dtos;

public class CharacterCreateDtoValidator : AbstractValidator<CharacterCreateDto>
{
    public CharacterCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.")
            .Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("Name can only contain letters, numbers, and spaces.");

        RuleFor(x => x.Game)
            .NotEmpty().WithMessage("Game is required.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Role must be a valid enum value.");
    }
    private bool BeValidRoleForGame(CharacterCreateDto dto, CharacterCreateDto.CharacterRole role)
    {
            if (dto.Game == "Rust" && (role == CharacterCreateDto.CharacterRole.Dachlose || role == CharacterCreateDto.CharacterRole.Toproof || role == CharacterCreateDto.CharacterRole.BigBoss))
            {
                return true; // Valid role for this game
            }
            if ((dto.Game == "CS2") && (role == CharacterCreateDto.CharacterRole.Antagonist || role == CharacterCreateDto.CharacterRole.Supporting || role == CharacterCreateDto.CharacterRole.Protagonist))
            {
                return true; // Valid role for this game
            }
    
            return false; // Invalid role for the specified game
    }
}