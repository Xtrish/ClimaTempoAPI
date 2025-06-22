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
        
        public async Task<PrevisaoModel?> ObterPrevisaoAsync(string cidade, int? dias = 5)
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