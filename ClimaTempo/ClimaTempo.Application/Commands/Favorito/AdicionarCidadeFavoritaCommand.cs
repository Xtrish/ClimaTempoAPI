using MediatR;

namespace ClimaTempo.Application.Commands.Favorito
{
    public class AdicionarCidadeFavoritaCommand : IRequest<Unit>
    {
        public int IdUsuario { get; set; } = 1; // fixo por enquanto
        public string Nome { get; set; } = string.Empty;
    }
}
