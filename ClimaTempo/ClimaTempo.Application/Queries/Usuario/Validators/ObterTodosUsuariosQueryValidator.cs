using ClimaTempo.Application.Queries.Usuario;
using FluentValidation;

namespace ClimaTempo.Application.Queries.Usuario.Validators
{
    public class ObterTodosUsuariosQueryValidator : AbstractValidator<ObterTodosUsuariosQuery>
    {
        public ObterTodosUsuariosQueryValidator()
        {

        }
    }
}
