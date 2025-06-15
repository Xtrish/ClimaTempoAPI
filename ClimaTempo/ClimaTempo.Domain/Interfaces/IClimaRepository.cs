using ClimaTempo.Domain.Models;

namespace ClimaTempo.Domain.Interfaces
{
    public interface IClimaRepository
    {
        Task<List<PrevisaoAtualModel?>> ObterPrevisaoAsync(string cidade, int dias);
        Task<PrevisaoAtualModel?> ObterPrevisaoDiariaAsync(string cidade);
    }
}