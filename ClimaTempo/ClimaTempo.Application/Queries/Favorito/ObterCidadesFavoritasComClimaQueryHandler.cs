using ClimaTempo.Application.Models;
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
            var favoritos = await _cidadeFavoritaRepository.ObterFavoritosproIdUsuarioAsync(request.IdUsuario);

            var tarefas = favoritos.Select(async cidade =>
            {
                var clima = await _climaService.ObterPrevisaoDiariaAsync(cidade.Nome);

                return clima is not null
                ? new FavoritoComClimaModel
                {
                    Id = cidade.IdCidadeFavorita,
                    Nome = cidade.Nome,       
                    TemperaturaCelsius = clima.TemperaturaAtual, 
                    TemperaturaMax = clima.TemperaturaMax,
                    TemperaturaMin = clima.TemperaturaMin,
                    Umidade = clima.Umidade,
                    Descricao = clima.Condicao,
                    Icone = clima.Icone,
                    Chuva = clima.Chuva
                }
                : null;
                
            });

            return (await Task.WhenAll(tarefas)).Where(x => x != null)!;
        }
    }
}
