using ClimaTempo.Application.Commands.Usuario;
using ClimaTempo.Application.Models;
using ClimaTempo.Application.Queries.Usuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClimaTempo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UsuariosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _mediator.Send(new ObterTodosUsuariosQuery());

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsuario(int id)
        {
            var usuario = await _mediator.Send(new ObterUsuarioPorIdQuery(id));

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] CriarUsuarioCommand usuario)
        {
            var id = await _mediator.Send(usuario);

            return CreatedAtAction(nameof(GetUsuario), new { id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioModel usuarioAtualizado)
        {
            var command = new AtualizarUsuarioCommand(id, usuarioAtualizado.Nome, usuarioAtualizado.Email);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            await _mediator.Send(new RemoverUsuarioCommand(id));

            return NoContent();
        }
    }
}
