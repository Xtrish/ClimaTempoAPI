using ClimaTempo.API.Data.Domain.Entities;
using ClimaTempo.API.Models;

namespace ClimaTempo.API.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> AdicionarAsync(Usuario usuario);
        Task<Usuario?> AtualizarAsync(Usuario usuario);
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task<Usuario?> ObterPorIdAsync(int id);
        Task<IEnumerable<Usuario>> ObterTodosAsync();
        Task<bool> RemoverAsync(int id);
    }
}