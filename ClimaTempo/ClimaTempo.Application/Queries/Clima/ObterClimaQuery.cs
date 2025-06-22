using ClimaTempo.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaTempo.Application.Queries.Clima
{
    public class ObterClimaQuery : IRequest<PrevisaoAtualModel>
    {
        public string Cidade { get; }
        public ObterClimaQuery(string cidade)
        {
            Cidade = cidade;
        }
    }
}
