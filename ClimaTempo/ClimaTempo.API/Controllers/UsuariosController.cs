using ClimaTempo.Application.Commands.Usuario;
using ClimaTempo.Application.Helpers;
using ClimaTempo.Application.Models;
using ClimaTempo.Application.Queries.Usuario;
using ClimaTempo.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ClimaTempo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public UsuariosController(IMediator mediator, IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _mediator = mediator;
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(model.Email);

            if (usuario == null || !SenhaHelper.VerificarSenha(model.Senha, usuario.SenhaHash))
                return Unauthorized("Usuário ou senha inválidos.");

            var token = JwtHelper.GerarToken(usuario.IdUsuario, usuario.Email, _configuration);
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("meteste")]
        public IActionResult ObterIdUsuario()
        {
            var id = int.Parse(User.FindFirst("IdUsuario")!.Value);
            return Ok(new { IdUsuario = id });
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
