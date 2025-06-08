namespace ClimaTempo.API.Models
{
    public class FavoritoComClimaModel
    {
        public string Nome { get; set; }
        public double TemperaturaCelsius { get; set; }
        public int Umidade { get; set; }
        public string Descricao { get; set; }
        public string Icone { get; set; }
    }
}
