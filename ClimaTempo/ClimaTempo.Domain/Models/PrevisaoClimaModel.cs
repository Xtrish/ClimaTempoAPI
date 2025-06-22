using System.Text.Json.Serialization;

namespace ClimaTempo.Domain.Models
{
    public class PrevisaoClima
    {
        [JsonPropertyName("location")]
        public Localizacao Localizacao { get; set; }

        [JsonPropertyName("current")]
        public ClimaAtual ClimaAtual { get; set; }
    }

    public class Localizacao
    {
        [JsonPropertyName("name")]
        public string Nome { get; set; }

        [JsonPropertyName("region")]
        public string Regiao { get; set; }

        [JsonPropertyName("country")]
        public string Pais { get; set; }
    }

    public class ClimaAtual
    {
        [JsonPropertyName("temp_c")]
        public decimal TemperaturaCelsius { get; set; }

        [JsonPropertyName("humidity")]
        public int Umidade { get; set; }

        [JsonPropertyName("condition")]
        public Condicao Condicao { get; set; }
    }

    public class Condicao
    {
        [JsonPropertyName("text")]
        public string Descricao { get; set; }

        [JsonPropertyName("icon")]
        public string Icone { get; set; }
    }

}
