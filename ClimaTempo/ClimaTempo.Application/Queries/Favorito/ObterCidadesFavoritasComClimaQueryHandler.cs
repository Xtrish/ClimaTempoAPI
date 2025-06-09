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
                var clima = await _climaService.ObterClimaAsync(cidade.Nome);

                return clima is not null
                    ? new FavoritoComClimaModel
                    {
                        Nome = cidade.Nome,
                        TemperaturaCelsius = clima.ClimaAtual.TemperaturaCelsius,
                        Umidade = clima.ClimaAtual.Umidade,
                        Descricao = clima.ClimaAtual.Condicao.Descricao,
                        Icone = clima.ClimaAtual.Condicao.Icone
                    }
                    : null;
            });

            return (await Task.WhenAll(tarefas)).Where(x => x != null)!;
        }
    }
}
