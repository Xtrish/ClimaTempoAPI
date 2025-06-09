using ClimaTempo.Application.Commands.Usuario;
using FluentValidation;

namespace ClimaTempo.Application.Commands.Usuario.Validators
{
    public class RemoverUsuarioCommandValidator : AbstractValidator<RemoverUsuarioCommand>
    {
        public RemoverUsuarioCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id inválido.");
        }
    }
}