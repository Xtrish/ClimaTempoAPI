using FluentValidation;

namespace ClimaTempo.Application.Commands.Favorito.Validators
{
    public class RemoverCidadeFavoritaCommandValidator : AbstractValidator<RemoverCidadeFavoritaCommand>
    {
        public RemoverCidadeFavoritaCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id da cidade favorita inválido.");
        }
    }
}
