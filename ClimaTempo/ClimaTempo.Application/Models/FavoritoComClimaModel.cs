namespace ClimaTempo.Application.Models
{
    public class FavoritoComClimaModel
    {
        public string Nome { get; set; }
        public decimal? TemperaturaCelsius { get; set; }
        public int Umidade { get; set; }
        public string Descricao { get; set; }
        public string Icone { get; set; }
        public double TemperaturaMax { get; internal set; }
        public double TemperaturaMin { get; internal set; }
        public string Chuva { get; internal set; }
        public int Id { get; set; }

    }
}
