using ClimaTempo.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClimaTempo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimaController : ControllerBase
    {
        private readonly IClimaRepository _climaService;

        public ClimaController(IClimaRepository climaService)
        {
            _climaService = climaService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObterClima([FromQuery] string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
                return BadRequest("Informe o nome da cidade.");

            var clima = await _climaService.ObterClimaAsync(cidade);

            if (clima is null)
                return NotFound("Cidade não encontrada ou erro ao consultar clima.");

            return Ok(clima);
        }

        [HttpGet("previsao")]
        public async Task<IActionResult> ObterPrevisao([FromQuery] string cidade, [FromQuery] int dias = 5)
        {
            if (string.IsNullOrWhiteSpace(cidade))
                return BadRequest("Informe a cidade.");

            if (dias < 1 || dias > 10)
                return BadRequest("O número de dias deve estar entre 1 e 10.");

            var previsao = await _climaService.ObterPrevisaoAsync(cidade, dias);

            return previsao is not null ? Ok(previsao) : NotFound("Não foi possível obter a previsão.");
        }

    }
}
