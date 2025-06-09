using ClimaTempo.Domain.Interfaces;
using MediatR;

namespace ClimaTempo.Application.Commands.Usuario
{
    public class AtualizarUsuarioCommandHandler : IRequestHandler<AtualizarUsuarioCommand, Unit>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AtualizarUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Unit> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuarioExistente = await _usuarioRepository.ObterPorEmailAsync(request.Email);

            if (usuarioExistente == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            usuarioExistente.Nome = request.Nome;
            usuarioExistente.Email = request.Email;

            await _usuarioRepository.AtualizarAsync(usuarioExistente);

            return Unit.Value;
        }
    }
}
