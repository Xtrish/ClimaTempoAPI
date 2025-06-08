using ClimaTempo.Domain.Entities;

namespace ClimaTempo.Domain.Interfaces
{
    public interface ICidadeFavoritaRepository
    {
        Task AdicionarFavoritoAsync(CidadeFavorita cidade);
        Task<CidadeFavorita[]> ObterFavoritosproIdUsuarioAsync(long idUsuario);
        Task RemoveridCidadeFavoritaAsync(long idCidadeFavorita);
    }
}