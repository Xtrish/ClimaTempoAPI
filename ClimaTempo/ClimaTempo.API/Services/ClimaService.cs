using ClimaTempo.API.Domain.Interfaces;
using ClimaTempo.API.Models;

namespace ClimaTempo.API.Services
{
    public class ClimaService : IClimaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ClimaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<PrevisaoClima?> ObterClimaAsync(string cidade)
        {
            var apiKey = _configuration["WeatherApi:ApiKey"];
            if (string.IsNullOrEmpty(apiKey)) return null;

            var url = $"https://api.weatherapi.com/v1/current.json?key={apiKey}&q={cidade}&lang=pt";

            try
            {
                return await _httpClient.GetFromJsonAsync<PrevisaoClima>(url);
            }
            catch
            {
                return null;
            }
        }
        public async Task<PrevisaoModel?> ObterPrevisaoAsync(string cidade, int dias)
        {
            var apiKey = _configuration["WeatherApi:ApiKey"];
            var url = $"https://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={cidade}&days={dias}&lang=pt";

            try
            {
                return await _httpClient.GetFromJsonAsync<PrevisaoModel>(url);
            }
            catch
            {
                return null;
            }
        }
    }
}
