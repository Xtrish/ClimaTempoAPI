using ClimaTempo.Domain.Interfaces;
using ClimaTempo.Domain.Models;
using MediatR;

namespace ClimaTempo.Application.Queries.Clima
{
    internal class ObterClimaQueryHandler : IRequestHandler<ObterClimaQuery, PrevisaoAtualModel>
    {
        private readonly IClimaRepository _climaService;
        public ObterClimaQueryHandler(IClimaRepository climaService)
        {
            _climaService = climaService;
        }
        public async Task<PrevisaoAtualModel> Handle(ObterClimaQuery request, CancellationToken cancellationToken)
        {

            var previsao = await _climaService.ObterPrevisaoAsync(request.Cidade, 1);

            if (previsao is null || !previsao.Any())
            {
                throw new KeyNotFoundException($"Previsão para a cidade '{request.Cidade}' não encontrada.");
            }

            return  previsao.First();
        }
    }    
}
