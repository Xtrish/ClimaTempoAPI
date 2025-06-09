using FluentValidation;

namespace ClimaTempo.Application.Commands.Favorito.Validators
{
    public class AdicionarCidadeFavoritaCommandValidator : AbstractValidator<AdicionarCidadeFavoritaCommand>
    {
        public AdicionarCidadeFavoritaCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome da cidade é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da cidade deve ter no máximo 100 caracteres.");

            RuleFor(x => x.IdUsuario)
                .GreaterThan(0).WithMessage("Id do usuário inválido.");
        }
    }
}
