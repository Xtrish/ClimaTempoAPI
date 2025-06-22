using ClimaTempo.Domain.Models;

namespace ClimaTempo.Domain.Interfaces
{
    public interface IClimaRepository
    {
        Task<PrevisaoModel?> ObterPrevisaoAsync(string cidade, int? dias = null);
    }
}