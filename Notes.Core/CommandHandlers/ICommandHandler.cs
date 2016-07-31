using System.Threading.Tasks;

namespace Notes.Core.CommandHandlers
{
    public interface ICommandHandler<TCommand>
    {
        Task<object> HandleAsync(TCommand command);
    }
}