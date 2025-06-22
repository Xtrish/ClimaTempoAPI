using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaTempo.Application.Queries.Clima.Validators
{
    internal class ObterClimaQueryValidator : AbstractValidator<ObterClimaQuery>
    {
        public ObterClimaQueryValidator()
        {
            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("Cidade é obrigatória.")
                .MinimumLength(3).WithMessage("Cidade deve ter pelo menos 3 caracteres.")
                .MaximumLength(100).WithMessage("Cidade deve ter no máximo 100 caracteres.");
        }
    }
}
