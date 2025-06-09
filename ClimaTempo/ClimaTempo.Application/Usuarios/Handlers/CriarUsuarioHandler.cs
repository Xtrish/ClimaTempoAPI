using ClimaTempo.Application.Helpers;
using ClimaTempo.Application.Usuarios.Commands;
using ClimaTempo.Domain.Entities;
using ClimaTempo.Domain.Interfaces;
using MediatR;

namespace ClimaTempo.Application.Usuarios.Handlers
{
    public class CriarUsuarioHandler : IRequestHandler<CriarUsuarioCommand, int>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CriarUsuarioHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<int> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = SenhaHelper.GerarHash(request.Senha)
            };

            var novo = await _usuarioRepository.AdicionarAsync(usuario);
            return novo.IdUsuario;
        }
    }

}
