using ClimaTempo.API.Data.Data;
using ClimaTempo.API.Data.Domain.Entities;
using ClimaTempo.API.Domain.Interfaces;
using ClimaTempo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClimaTempo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritosController : ControllerBase
    {
        private readonly ICidadeFavoritaRepository _cidadeFavoritaRepository;
        private readonly IClimaRepository _climaService;

        public FavoritosController(IClimaRepository climaService, ICidadeFavoritaRepository cidadeFavoritaRepository)
        {
            _climaService = climaService;
            _cidadeFavoritaRepository = cidadeFavoritaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterFavoritos()
        {
            var favoritos = await _cidadeFavoritaRepository.ObterFavoritosproIdUsuarioAsync(1);

            var tarefas =  favoritos.Select(async cidade =>
            {
                var clima = await _climaService.ObterClimaAsync(cidade.Nome);

                return clima is not null
                    ? new FavoritoComClimaModel
                    {
                        Nome = cidade.Nome,
                        TemperaturaCelsius = clima.ClimaAtual.TemperaturaCelsius,
                        Umidade = clima.ClimaAtual.Umidade,
                        Descricao = clima.ClimaAtual.Condicao.Descricao,
                        Icone = clima.ClimaAtual.Condicao.Icone
                    }
                    : null;
            });

            var resultado = (await Task.WhenAll(tarefas)).Where(r => r != null);

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarFavorito([FromBody] CidadeFavoritaModel cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade.Nome))
                return BadRequest("Nome da cidade é obrigatório.");

            var entidade = new CidadeFavorita { IdUsuario = 1, Nome = cidade.Nome };
            await _cidadeFavoritaRepository.AdicionarFavoritoAsync(entidade);

            return Created(string.Empty, cidade);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverFavorito(int id)
        {
            await _cidadeFavoritaRepository.RemoveridCidadeFavoritaAsync(id);
            return NoContent();
        }
    }
}
