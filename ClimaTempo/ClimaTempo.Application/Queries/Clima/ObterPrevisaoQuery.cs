using ClimaTempo.Domain.Models;
using MediatR;

namespace ClimaTempo.Application.Queries.Clima
{
    public record ObterPrevisaoQuery(string Cidade, int Dias) : IRequest<List<PrevisaoAtualModel>>;
}
