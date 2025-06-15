using ClimaTempo.Domain.Interfaces;
using ClimaTempo.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net.Http.Json;

namespace ClimaTempo.Infrastructure.Repositories
{
    public class WeatherApiRepository : IClimaRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeatherApiRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<PrevisaoAtualModel?> ObterPrevisaoDiariaAsync(string cidade)
        {
            var apiKey = _configuration["WeatherApi:ApiKey"];
            var url = $"https://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={cidade}&days=1&lang=pt";

            try
            {
                var previsao = await _httpClient.GetFromJsonAsync<PrevisaoModel>(url);

                if (previsao == null || previsao.Forecast == null || previsao.Forecast.ForecastDay == null)
                    return null;

                var hoje = previsao.Forecast.ForecastDay.First();

                return new PrevisaoAtualModel
                {
                    Cidade = cidade,
                    TemperaturaAtual = previsao.Current.TemperaturaCelsius,
                    TemperaturaMin = hoje.Day.MintempC,
                    TemperaturaMax = hoje.Day.MaxtempC,
                    Umidade = previsao.Current.Umidade,
                    Condicao = previsao.Current.Condicao.Descricao,
                    Icone = "https:" + previsao.Current.Condicao.Icone,
                    Chuva = $"{hoje.Day.TotalPrecipMm}mm - {hoje.Day.DailyChanceOfRain}%"
                };

            }
            catch
            {
                return null;
            }
        }

        public async Task<List<PrevisaoAtualModel>> ObterPrevisaoAsync(string cidade, int dias)
        {
            var apiKey = _configuration["WeatherApi:ApiKey"];
            var url = $"https://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={cidade}&days={dias}&lang=pt";

            try
            {
                var previsao = await _httpClient.GetFromJsonAsync<PrevisaoModel>(url);

                if (previsao == null || previsao.Forecast?.ForecastDay == null)
                    return new();

                return previsao.Forecast.ForecastDay.Select(dia => new PrevisaoAtualModel
                {
                    Data = dia.Date,
                    Cidade = cidade,
                    TemperaturaAtual = previsao.Current.TemperaturaCelsius,
                    TemperaturaMin = dia.Day.MintempC,
                    TemperaturaMax = dia.Day.MaxtempC,
                    Umidade = previsao.Current.Umidade,
                    Condicao = dia.Day.Condicao.Descricao ?? "N/A",
                    Icone = "https:" + previsao.Current.Condicao.Icone,
                    Chuva = $"{dia.Day.TotalPrecipMm}mm - {dia.Day.DailyChanceOfRain}%"
                }).ToList();
            }
            catch
            {
                return new();
            }
        }
    }
}