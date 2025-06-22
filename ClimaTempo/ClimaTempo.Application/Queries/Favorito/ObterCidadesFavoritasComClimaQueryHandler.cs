using ClimaTempo.Application.Helpers;
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

            if (favoritos == null || !favoritos.Any())
                return Enumerable.Empty<FavoritoComClimaModel>();

            var tarefas = favoritos.Select(async cidade => await ObterFavoritoComClimaAsync(cidade));

            var resultados = await Task.WhenAll(tarefas);

            return resultados.Where(r => r != null)!;
        }

        private async Task<FavoritoComClimaModel?> ObterFavoritoComClimaAsync(CidadeFavorita cidade)
        {
            var previsao = await _climaService.ObterPrevisaoAsync(cidade.Nome, 1);

            if (previsao?.Forecast?.ForecastDay == null)
                throw new KeyNotFoundException($"Previsão para a cidade '{cidade.Nome}' não encontrada.");

            var lista = PrevisaoHelper.MapearParaPrevisoes(previsao, cidade.Nome);
            var dia = lista.FirstOrDefault();

            if (dia == null)
                return null;

            return new FavoritoComClimaModel
            {
                Id = cidade.IdCidadeFavorita,
                Nome = cidade.Nome,
                TemperaturaCelsius = dia.TemperaturaAtual,
                TemperaturaMax = dia.TemperaturaMax,
                TemperaturaMin = dia.TemperaturaMin,
                Umidade = dia.Umidade,
                Descricao = dia.Condicao,
                Icone = dia.Icone,
                Chuva = dia.Chuva
            };
        }
    }
}
