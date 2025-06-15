using System.Text.Json.Serialization;
namespace ClimaTempo.Domain.Models
{
    public class PrevisaoModel
    {
        [JsonPropertyName("location")]
        public Localizacao Location { get; set; }

        [JsonPropertyName("current")]
        public ClimaAtual Current { get; set; }

        [JsonPropertyName("forecast")]
        public PrevisaoForecast Forecast { get; set; }
    }


    public class PrevisaoForecast
    {
        [JsonPropertyName("forecastday")]
        public List<PrevisaoDia> ForecastDay { get; set; }
    }


    public class PrevisaoDia
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("day")]
        public PrevisaoDiaDetalhe Day { get; set; }
    }


    public class PrevisaoDiaDetalhe
    {
        [JsonPropertyName("maxtemp_c")]
        public double MaxtempC { get; set; }

        [JsonPropertyName("mintemp_c")]
        public double MintempC { get; set; }

        [JsonPropertyName("totalprecip_mm")]
        public double TotalPrecipMm { get; set; }

        [JsonPropertyName("daily_chance_of_rain")]
        public int DailyChanceOfRain { get; set; }

        [JsonPropertyName("condition")]
        public Condicao Condicao { get; set; }
    }

}
