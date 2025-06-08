using ClimaTempo.API.Data.Data;
using ClimaTempo.API.Data.Domain.Entities;
using ClimaTempo.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClimaTempo.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> ObterTodosAsync()
        {
            return await _context.Usuario.ToListAsync();
        }

        public async Task<Usuario?> ObterPorIdAsync(int id)
        {
            return await _context.Usuario.FindAsync(id);
        }

        public async Task<Usuario> AdicionarAsync(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario?> AtualizarAsync(Usuario usuario)
        {
            var usuarioDB = await _context.Usuario.FindAsync(usuario.IdUsuario);
            if (usuarioDB == null)
                return null;

            _context.Entry(usuarioDB).CurrentValues.SetValues(usuario);
            await _context.SaveChangesAsync();
            return usuarioDB;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return false;

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
