using ClimaTempo.Application.Models;
using MediatR;

namespace ClimaTempo.Application.Queries.Usuario
{
    public class ObterUsuarioPorIdQuery : IRequest<UsuarioModel?>
    {
        public int Id { get; set; }

        public ObterUsuarioPorIdQuery(int id)
        {
            Id = id;
        }
    }
}