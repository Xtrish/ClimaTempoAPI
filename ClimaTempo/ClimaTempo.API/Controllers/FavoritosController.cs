using ClimaTempo.API.Data.Data;
using ClimaTempo.API.Domain.Entities;
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
        private readonly AppDbContext _context;
        private readonly IClimaService _climaService;

        public FavoritosController(AppDbContext context, IClimaService climaService)
        {
            _context = context;
            _climaService = climaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterFavoritos()
        {
            var favoritos = await _context.CidadesFavoritas.ToListAsync();

            var tarefas = favoritos.Select(async cidade =>
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

            var entidade = new CidadesFavoritas { Nome = cidade.Nome };
            _context.CidadesFavoritas.Add(entidade);
            await _context.SaveChangesAsync();

            return Created(string.Empty, cidade);        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverFavorito(int id)
        {
            var cidade = await _context.CidadesFavoritas.FindAsync(id);
            if (cidade is null)
                return NotFound("Cidade não encontrada.");

            _context.CidadesFavoritas.Remove(cidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
