using ClimaTempo.Application.Commands.Favorito;
using ClimaTempo.Application.Models;
using ClimaTempo.Application.Queries.Favorito;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClimaTempo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FavoritosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ObterFavoritos()
        {
            var query = new ObterCidadesFavoritasComClimaQuery(1);
            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarFavorito([FromBody] CidadeFavoritaModel cidade)
        {
            var command = new AdicionarCidadeFavoritaCommand { Nome = cidade.Nome };
            await _mediator.Send(command);
            return Created(string.Empty, cidade);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverFavorito(int id)
        {
            await _mediator.Send(new RemoverCidadeFavoritaCommand(id));
            return NoContent();
        }
    }
}