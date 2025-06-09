using ClimaTempo.Application.Models;
using ClimaTempo.Application.Usuarios.Commands;
using ClimaTempo.Domain.Entities;
using ClimaTempo.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClimaTempo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository ;
        private readonly IMediator _mediator;

        public UsuariosController(IMediator mediator, IUsuarioRepository usuarioRepository)
        {
            _mediator = mediator;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuariosDB = await _usuarioRepository.ObterTodosAsync();

            var usuariosModel = usuariosDB.Select(u => new UsuarioModel
            {
                Nome = u.Nome,
                Email = u.Email
            }).ToList();

            return Ok(usuariosModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsuario(int id)
        {
            var usuarioDB = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuarioDB == null)
                return NotFound();

            var usuario = new UsuarioModel
            {
                Nome = usuarioDB.Nome,
                Email = usuarioDB.Email
            };

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
            var usuario = new Usuario
            {
                IdUsuario = id,
                Nome = usuarioAtualizado.Nome,
                Email = usuarioAtualizado.Email,
            };

            var usuarioExistente = await _usuarioRepository.ObterPorEmailAsync(usuario.Email);

            if (usuarioExistente == null)
                return NotFound();

            usuarioExistente.Nome = usuarioAtualizado.Nome;
            usuarioExistente.Email = usuarioAtualizado.Email;

            await _usuarioRepository.AtualizarAsync(usuarioExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _usuarioRepository.RemoverAsync(id);

            return NoContent();
        }
    }
}
