using ClimaTempo.API.Models;

namespace ClimaTempo.API.Domain.Interfaces
{
    public interface IClimaService
    {
        Task<PrevisaoClima?> ObterClimaAsync(string cidade);
        Task<PrevisaoModel?> ObterPrevisaoAsync(string cidade, int dias);

    }
}