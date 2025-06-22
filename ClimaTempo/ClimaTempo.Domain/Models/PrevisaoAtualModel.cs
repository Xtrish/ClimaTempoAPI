using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaTempo.Domain.Models
{
    public class PrevisaoAtualModel
    {
        public int Id { get; set; }
        public string Cidade { get; set; }
        public double TemperaturaAtual { get; set; }
        public double TemperaturaMin { get; set; }
        public double TemperaturaMax { get; set; }
        public int Umidade { get; set; }
        public string Condicao { get; set; }
        public string Icone { get; set; }
        public string Chuva { get; set; }
        public string Data { get; set; }
    }

}
