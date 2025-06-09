using ClimaTempo.Application.Queries.Favorito;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaTempo.Application.Commands.Favorito.Validators
{
    public class ObterCidadesFavoritasComClimaQueryValidator : AbstractValidator<ObterCidadesFavoritasComClimaQuery>
    {
        public ObterCidadesFavoritasComClimaQueryValidator()
        {
            RuleFor(x => x.IdUsuario)
                .GreaterThan(0).WithMessage("Id do usuário inválido.");
        }
    }
}
