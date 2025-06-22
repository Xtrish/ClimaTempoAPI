using ClimaTempo.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaTempo.Application.Commands.Favorito
{
    public class RemoverCidadeFavoritaCommandHandler : IRequestHandler<RemoverCidadeFavoritaCommand,Unit>
    {
        private readonly ICidadeFavoritaRepository _cidadeFavoritaRepository;

        public RemoverCidadeFavoritaCommandHandler(ICidadeFavoritaRepository cidadeFavoritaRepository)
        {
            _cidadeFavoritaRepository = cidadeFavoritaRepository;
        }

        public async Task<Unit> Handle(RemoverCidadeFavoritaCommand request, CancellationToken cancellationToken)
        {
            await _cidadeFavoritaRepository.RemoverIdCidadeFavoritaAsync(request.Id);
            return Unit.Value;
        }
    }
}
