using ClimaTempo.Domain.Entities;
using ClimaTempo.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaTempo.Application.Commands.Favorito
{
    public class AdicionarCidadeFavoritaCommandHandler : IRequestHandler<AdicionarCidadeFavoritaCommand, Unit>
    {
        private readonly ICidadeFavoritaRepository _cidadeFavoritaRepository;

        public AdicionarCidadeFavoritaCommandHandler(ICidadeFavoritaRepository cidadeFavoritaRepository)
        {
            _cidadeFavoritaRepository = cidadeFavoritaRepository;
        }

        public async Task<Unit> Handle(AdicionarCidadeFavoritaCommand request, CancellationToken cancellationToken)
        {
            var entidade = new CidadeFavorita
            {
                IdUsuario = request.IdUsuario,
                Nome = request.Nome
            };

            await _cidadeFavoritaRepository.AdicionarFavoritoAsync(entidade);

            return Unit.Value;
        }
    }

}
