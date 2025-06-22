using FluentValidation;

namespace ClimaTempo.Application.Queries.Clima.Validators
{
    public class ObterPrevisaoQueryValidator : AbstractValidator<ObterPrevisaoQuery>
    {
        public ObterPrevisaoQueryValidator()
        {
            RuleFor(x => x.Cidade)
                .NotEmpty()
                .WithMessage("O nome da cidade deve ser informado.");

            RuleFor(x => x.Dias)
                .InclusiveBetween(1, 10)
                .WithMessage("O número de dias deve estar entre 1 e 10.");
        }
    }
}
