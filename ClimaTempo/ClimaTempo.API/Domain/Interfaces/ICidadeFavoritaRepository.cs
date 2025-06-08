using ClimaTempo.API.Data.Domain.Entities;

namespace ClimaTempo.API.Domain.Interfaces
{
    public interface ICidadeFavoritaRepository
    {
        Task AdicionarFavoritoAsync(CidadeFavorita cidade);
        Task<CidadeFavorita[]> ObterFavoritosproIdUsuarioAsync(long idUsuario);
        Task RemoveridCidadeFavoritaAsync(long idCidadeFavorita);
    }
}