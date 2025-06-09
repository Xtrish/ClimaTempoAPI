using MediatR;
using ClimaTempo.Application.Models;

namespace ClimaTempo.Application.Queries.Usuario
{
    public class ObterTodosUsuariosQuery : IRequest<UsuarioModel[]>
    {
    }
}
