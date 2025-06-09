using MediatR;

namespace ClimaTempo.Application.Commands.Usuario
{
    public class AtualizarUsuarioCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public AtualizarUsuarioCommand(int id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }
    }
}
