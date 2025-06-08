using ClimaTempo.Application.Models;

namespace ClimaTempo.Domain.Interfaces
{
    public interface IClimaRepository
    {
        Task<PrevisaoClima?> ObterClimaAsync(string cidade);
        Task<PrevisaoModel?> ObterPrevisaoAsync(string cidade, int dias);

    }
}