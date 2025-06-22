using ClimaTempo.Domain.Interfaces;
using ClimaTempo.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaTempo.Application.Queries.Clima
{
    internal class ObterPrevisaoQueryHandler : IRequestHandler<ObterPrevisaoQuery, List<PrevisaoAtualModel>>
    {
        private readonly IClimaRepository _climaService;

        public ObterPrevisaoQueryHandler(IClimaRepository climaService)
        {
            _climaService = climaService;
        }

        public async Task<List<PrevisaoAtualModel>> Handle(ObterPrevisaoQuery request, CancellationToken cancellationToken)
        {
            var previsao = await _climaService.ObterPrevisaoAsync(request.Cidade, request.Dias);

            if (previsao is null )
            {
                throw new KeyNotFoundException($"Não foi possível obter a previsão para a cidade '{request.Cidade}'.");
            }

            return previsao;
        }
    }
}
