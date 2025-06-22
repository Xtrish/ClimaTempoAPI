using ClimaTempo.Application.Models;
using ClimaTempo.Domain.Entities;
using ClimaTempo.Domain.Interfaces;
using MediatR;

namespace ClimaTempo.Application.Queries.Favorito
{
    public class ObterCidadesFavoritasComClimaQueryHandler : IRequestHandler<ObterCidadesFavoritasComClimaQuery, IEnumerable<FavoritoComClimaModel>>
    {
        private readonly ICidadeFavoritaRepository _cidadeFavoritaRepository;
        private readonly IClimaRepository _climaService;

        public ObterCidadesFavoritasComClimaQueryHandler(ICidadeFavoritaRepository cidadeFavoritaRepository, IClimaRepository climaService)
        {
            _cidadeFavoritaRepository = cidadeFavoritaRepository;
            _climaService = climaService;
        }

        public async Task<IEnumerable<FavoritoComClimaModel>> Handle(ObterCidadesFavoritasComClimaQuery request, CancellationToken cancellationToken)
        {
            var favoritos = await _cidadeFavoritaRepository.ObterFavoritosPorIdUsuarioAsync(request.IdUsuario);

            var tarefas = favoritos.Select(cidade => ObterFavoritoComClimaAsync(cidade));

            var resultados = await Task.WhenAll(tarefas);         

            return resultados;
        }

        private async Task<FavoritoComClimaModel?> ObterFavoritoComClimaAsync(CidadeFavorita cidade)
        {
            var previsoes = await _climaService.ObterPrevisaoAsync(cidade.Nome, 1);
            var previsao = previsoes?.FirstOrDefault();

            if (previsao is null)
            {
                return null;
            }

            return new FavoritoComClimaModel
            {
                Id = cidade.IdCidadeFavorita,
                Nome = cidade.Nome,
                TemperaturaCelsius = previsao.TemperaturaAtual,
                TemperaturaMax = previsao.TemperaturaMax,
                TemperaturaMin = previsao.TemperaturaMin,
                Umidade = previsao.Umidade,
                Descricao = previsao.Condicao,
                Icone = previsao.Icone,
                Chuva = previsao.Chuva
            };
        }
    }
}
