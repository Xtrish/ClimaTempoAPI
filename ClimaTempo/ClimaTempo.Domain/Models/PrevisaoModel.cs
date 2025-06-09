using System.Text.Json.Serialization;
namespace ClimaTempo.Domain.Models
{
    public class PrevisaoModel
    {
        [JsonPropertyName("location")]
        public Localizacao Localizacao { get; set; }

        [JsonPropertyName("forecast")]
        public Previsao Previsao { get; set; }
    }

    public class Previsao
    {
        [JsonPropertyName("forecastday")]
        public List<DiaPrevisao> Dias { get; set; }
    }

    public class DiaPrevisao
    {
        [JsonPropertyName("date")]
        public string Data { get; set; }

        [JsonPropertyName("day")]
        public InformacoesDoDia Dia { get; set; }
    }

    public class InformacoesDoDia
    {
        [JsonPropertyName("maxtemp_c")]
        public double TemperaturaMax { get; set; }

        [JsonPropertyName("mintemp_c")]
        public double TemperaturaMin { get; set; }

        [JsonPropertyName("avgtemp_c")]
        public double TemperaturaMed { get; set; }

        [JsonPropertyName("condition")]
        public Condicao Condicao { get; set; }
    }

}
