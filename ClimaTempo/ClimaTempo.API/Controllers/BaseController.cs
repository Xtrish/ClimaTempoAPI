using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClimaTempo.API.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected int? UsuarioId =>
            int.TryParse(User.FindFirst("IdUsuario")?.Value, out var id) ? id : null;

        protected async Task<IActionResult> ExecutarComUsuarioAutenticado(Func<int, Task<IActionResult>> acao)
        {
            if (UsuarioId is null)
                return Unauthorized("Token inválido");

            return await acao(UsuarioId.Value);
        }
    }
}
