using ClimaTempo.Application.Helpers;
using ClimaTempo.Domain.Models;

namespace ClimaTempo.Test.Application
{
    public class PrevisaoHelperTest
    {

        [Theory]
        [InlineData(15.0, 28.5, "10.0", 50, "10,0mm - 50%")]
        [InlineData(20.0, 32.0, "0.0", 0, "0,0mm - 0%")]
        [InlineData(12.3, 22.8, "5.5", 80, "5,5mm - 80%")]
        [InlineData(10.0, 25.0, null, null, "0,0mm - 0%")]
        public void Testa_ListaPrevisaoDeUmDia(double minTemp, double maxTemp, string? precipMmStr, int? chanceChuva, string esperadoChuva)
        {
            var precipMm = precipMmStr is null ?
                (decimal?)null
                : decimal.Parse(precipMmStr, System.Globalization.CultureInfo.InvariantCulture);
            
            var previsao = new PrevisaoModel
            {
                Current = new ClimaAtual { TemperaturaCelsius = 26.3m },
                Forecast = new PrevisaoForecast
                {
                    ForecastDay = new List<PrevisaoDia>
                    {
                        new PrevisaoDia
                        {
                            Date = "2025-06-22",
                            Day = new PrevisaoDiaDetalhe
                            {
                                MintempC = minTemp,
                                MaxtempC = maxTemp,
                                TotalPrecipMm = precipMm,
                                DailyChanceOfRain = chanceChuva,
                                Avghumidity = 70,
                                Condicao = new Condicao
                                {
                                    Descricao = "Ensolarado",
                                    Icone = "//cdn.weatherapi.com/weather/64x64/day/113.png"
                                }
                            }
                        }
                    }
                }
            };

            var cidade = "Rio de Janeiro";

            var resultado = PrevisaoHelper.MapearParaPrevisoes(previsao, cidade);

            Assert.Single(resultado);
            var previsaoAtual = resultado[0];
            Assert.Equal(minTemp, previsaoAtual.TemperaturaMin);
            Assert.Equal(maxTemp, previsaoAtual.TemperaturaMax);
            Assert.Equal(esperadoChuva, previsaoAtual.Chuva);
            Assert.Equal("https://cdn.weatherapi.com/weather/64x64/day/113.png", previsaoAtual.Icone);
            Assert.Equal("Ensolarado", previsaoAtual.Condicao);
        }

        [Fact]
        public void Testa_ListaComPrevisoesDeMultiplosDias()
        {
            var previsao = new PrevisaoModel
            {
                Current = new ClimaAtual { TemperaturaCelsius = 22.5m },
                Forecast = new PrevisaoForecast
                {
                    ForecastDay = new List<PrevisaoDia>
                    {
                        new PrevisaoDia
                        {
                            Date = "2025-06-22",
                            Day = new PrevisaoDiaDetalhe
                            {
                                MintempC = 15.0,
                                MaxtempC = 27.5,
                                TotalPrecipMm = 8.2m,
                                DailyChanceOfRain = 60,
                                Avghumidity = 75,
                                Condicao = new Condicao
                                {
                                    Descricao = "Chuva leve",
                                    Icone = "//cdn.weatherapi.com/weather/64x64/day/296.png"
                                }
                            }
                        },
                        new PrevisaoDia
                        {
                            Date = "2025-06-23",
                            Day = new PrevisaoDiaDetalhe
                            {
                                MintempC = 17.0,
                                MaxtempC = 29.0,
                                TotalPrecipMm = 0m,
                                DailyChanceOfRain = 10,
                                Avghumidity = 68,
                                Condicao = new Condicao
                                {
                                    Descricao = "Ensolarado",
                                    Icone = "//cdn.weatherapi.com/weather/64x64/day/113.png"
                                }
                            }
                        }
                    }
                }
            };

            var cidade = "Campinas";

            var resultado = PrevisaoHelper.MapearParaPrevisoes(previsao, cidade);

            Assert.Equal(2, resultado.Count);

            Assert.Collection(resultado,
                dia1 =>
                {
                    Assert.Equal("2025-06-22", dia1.Data);
                    Assert.Equal(15.0, dia1.TemperaturaMin);
                    Assert.Equal(27.5, dia1.TemperaturaMax);
                    Assert.Equal("Chuva leve", dia1.Condicao);
                    Assert.Equal("https://cdn.weatherapi.com/weather/64x64/day/296.png", dia1.Icone);
                    Assert.Equal("8,2mm - 60%", dia1.Chuva);
                },
                dia2 =>
                {
                    Assert.Equal("2025-06-23", dia2.Data);
                    Assert.Equal(17.0, dia2.TemperaturaMin);
                    Assert.Equal(29.0, dia2.TemperaturaMax);
                    Assert.Equal("Ensolarado", dia2.Condicao);
                    Assert.Equal("https://cdn.weatherapi.com/weather/64x64/day/113.png", dia2.Icone);
                    Assert.Equal("0,0mm - 10%", dia2.Chuva);
                }
            );
        }
    }
}