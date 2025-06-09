using MediatR;

namespace ClimaTempo.Application.Commands.Usuario
{
    public class CriarUsuarioCommand : IRequest<int>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
