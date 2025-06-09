using ClimaTempo.Domain.Interfaces;
using MediatR;

namespace ClimaTempo.Application.Commands.Usuario
{
    public class RemoverUsuarioCommandHandler : IRequestHandler<RemoverUsuarioCommand, Unit>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public RemoverUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Unit> Handle(RemoverUsuarioCommand request, CancellationToken cancellationToken)
        {

            var usuario = await _usuarioRepository.ObterPorIdAsync(request.Id);
            if (usuario == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            await _usuarioRepository.RemoverAsync(request.Id);
            return Unit.Value;
        }
    }
}
