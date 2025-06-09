using MediatR;

namespace ClimaTempo.Application.Commands.Usuario
{
    public class RemoverUsuarioCommand : IRequest<Unit>
    {
        public int Id { get; }

        public RemoverUsuarioCommand(int id)
        {
            Id = id;
        }
    }
}
