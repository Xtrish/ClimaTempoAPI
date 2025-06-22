using ClimaTempo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaTempo.Application.Helpers
{
    public static class PrevisaoHelper
    {
        public static List<PrevisaoAtualModel> MapearParaPrevisoes(PrevisaoModel previsao, string cidade)
        {
            var lista = new List<PrevisaoAtualModel>();

            foreach (var dia in previsao.Forecast.ForecastDay)
            {
                lista.Add(new PrevisaoAtualModel
                {
                    Data = dia.Date,
                    Cidade = cidade,
                    TemperaturaAtual = previsao.Current.TemperaturaCelsius,
                    TemperaturaMin = dia.Day.MintempC,
                    TemperaturaMax = dia.Day.MaxtempC,
                    Umidade = dia.Day.Avghumidity,
                    Condicao = dia.Day.Condicao.Descricao ?? "N/A",
                    Icone = "https:" + dia.Day.Condicao.Icone,
                    Chuva = $"{(dia.Day.TotalPrecipMm ?? 0).ToString("F1", new CultureInfo("pt-BR"))}mm - {(dia.Day.DailyChanceOfRain ?? 0)}%"
                });
            }

            return lista;
        }
    }    
}
