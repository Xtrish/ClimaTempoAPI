using ClimaTempo.API.Data.Data;
using ClimaTempo.API.Data.Domain.Entities;
using ClimaTempo.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClimaTempo.API.Repositories
{
    public class CidadeFavoritaRepository : ICidadeFavoritaRepository
    {

        private readonly AppDbContext _context;
        public CidadeFavoritaRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<CidadeFavorita[]> ObterFavoritosproIdUsuarioAsync(long idUsuario)
        {
            return await  _context.CidadeFavorita.Where(x=> x.IdUsuario == idUsuario).ToArrayAsync();
        }
        public async Task AdicionarFavoritoAsync(CidadeFavorita cidade)
        {
            _context.CidadeFavorita.Add(cidade);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveridCidadeFavoritaAsync(long idCidadeFavorita)
        {
            var favorito = await _context.CidadeFavorita
                .Where(x=> x.IdCidadeFavorita == idCidadeFavorita)
                .FirstAsync();

            _context.CidadeFavorita.Remove(favorito);
            await _context.SaveChangesAsync();
        }

    }
}
