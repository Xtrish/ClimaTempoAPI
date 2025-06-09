using MediatR;

namespace ClimaTempo.Application.Commands.Favorito
{
    public class RemoverCidadeFavoritaCommand : IRequest<Unit>
    {
        public int Id { get; }

        public RemoverCidadeFavoritaCommand(int id)
        {
            Id = id;
        }
    }
}
