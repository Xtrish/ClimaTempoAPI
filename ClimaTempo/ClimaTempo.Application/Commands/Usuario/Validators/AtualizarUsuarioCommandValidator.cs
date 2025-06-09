using ClimaTempo.Application.Commands.Usuario;
using FluentValidation;

namespace ClimaTempo.Application.Commands.Usuario.Validators
{
    public class AtualizarUsuarioCommandValidator : AbstractValidator<AtualizarUsuarioCommand>
    {
        public AtualizarUsuarioCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id inválido.");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");
        }
    }
}
