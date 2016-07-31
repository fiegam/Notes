using Nancy;
using Nancy.ModelBinding;
using Notes.Contract.Commands;
using Notes.Contract.Queries;
using Notes.Core.Infrastructure;
using Notes.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.WebApi.Modules
{
    public class NotesModule : NancyModule
    {
        private IHandlerFactory _queryHandlerFactory;

        public NotesModule(IHandlerFactory queryHandlerFactory) : base("notes")
        {
            _queryHandlerFactory = queryHandlerFactory;
            Get("/", async (_, token) => await HandleQuery(this.Bind<GetNotesQuery>()));
            Post("/", async (_,token) => await HandleCommand(this.Bind<SaveNoteCommand>()));
        }

        private async Task<object> HandleQuery<TQuery>(TQuery query)
        {
            var handler = _queryHandlerFactory.CreateQueryHandler<TQuery>();

            var result = await handler.HandleAsync(query);

            return result;
        }

        private async Task<object> HandleCommand<TCommand>(TCommand command)
        {
            var handler = _queryHandlerFactory.CreateCommandHandler<TCommand>();

            var result = await handler.HandleAsync(command);

            return result;
        }
    }
}