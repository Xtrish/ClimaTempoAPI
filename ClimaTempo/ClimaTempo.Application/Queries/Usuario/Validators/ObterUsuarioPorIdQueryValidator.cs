using ClimaTempo.Application.Queries.Usuario;
using FluentValidation;

namespace ClimaTempo.Application.Queries.Usuario.Validators
{
    public class ObterUsuarioPorIdQueryValidator : AbstractValidator<ObterUsuarioPorIdQuery>
    {
        public ObterUsuarioPorIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id Inválido.");
        }
    }
}