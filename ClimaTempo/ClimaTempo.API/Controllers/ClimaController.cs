using ClimaTempo.Application.Queries.Clima;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClimaTempo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimaController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ClimaController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObterClima([FromQuery] string cidade)
        {
            var query = new ObterClimaQuery(cidade);

            var clima = await _mediator.Send(query);

            return Ok(clima);
        }

        [HttpGet("previsao")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPrevisao([FromQuery] string cidade, [FromQuery] int dias = 5)
        {
            var query = new ObterPrevisaoQuery(cidade, dias);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
