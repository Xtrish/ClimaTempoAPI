using ClimaTempo.Application.Models;
using ClimaTempo.Domain.Interfaces;
using MediatR;

namespace ClimaTempo.Application.Queries.Usuario
{
    public class ObterUsuarioPorIdQueryHandler : IRequestHandler<ObterUsuarioPorIdQuery, UsuarioModel?>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ObterUsuarioPorIdQueryHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioModel?> Handle(ObterUsuarioPorIdQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(request.Id);

            if (usuario == null) return null;

            return new UsuarioModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }
    }
}
