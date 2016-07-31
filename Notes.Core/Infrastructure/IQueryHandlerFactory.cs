
using Notes.Core.CommandHandlers;
using Notes.Core.QueryHandlers;

namespace Notes.Core.Infrastructure
{
    public interface IHandlerFactory
    {
        IQueryHandler<TQuery> CreateQueryHandler<TQuery>();

        ICommandHandler<TCommand> CreateCommandHandler<TCommand>();
    }
}