using ClimaTempo.Application.Models;
using ClimaTempo.Domain.Interfaces;
using MediatR;

namespace ClimaTempo.Application.Queries.Usuario
{
    public class ObterTodosUsuariosQueryHandler : IRequestHandler<ObterTodosUsuariosQuery, UsuarioModel[]>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ObterTodosUsuariosQueryHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioModel[]> Handle(ObterTodosUsuariosQuery request, CancellationToken cancellationToken)
        {
            var usuariosDB = await _usuarioRepository.ObterTodosAsync();

            return usuariosDB.Select(u => new UsuarioModel
            {
                Nome = u.Nome,
                Email = u.Email
            }).ToArray();
        }
    }
}
