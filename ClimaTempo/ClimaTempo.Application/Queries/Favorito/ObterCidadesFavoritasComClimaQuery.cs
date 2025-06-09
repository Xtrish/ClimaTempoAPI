using ClimaTempo.Application.Models;
using MediatR;

namespace ClimaTempo.Application.Queries.Favorito
{
    public class ObterCidadesFavoritasComClimaQuery : IRequest<IEnumerable<FavoritoComClimaModel>>
    {
        public int IdUsuario { get; }

        public ObterCidadesFavoritasComClimaQuery(int idUsuario)
        {
            IdUsuario = idUsuario;
        }
    }
}
