using MediatR;

namespace ClimaTempo.Application.Usuarios.Commands
{
    public class CriarUsuarioCommand : IRequest<int>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
