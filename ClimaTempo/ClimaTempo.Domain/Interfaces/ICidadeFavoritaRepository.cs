using ClimaTempo.Domain.Entities;

namespace ClimaTempo.Domain.Interfaces
{
    public interface ICidadeFavoritaRepository
    {
        Task AdicionarFavoritoAsync(CidadeFavorita cidade);
        Task<CidadeFavorita[]> ObterFavoritosPorIdUsuarioAsync(long idUsuario);
        Task RemoverIdCidadeFavoritaAsync(long idCidadeFavorita);
    }
}