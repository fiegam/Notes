using Nancy;
using Ninject;
using Notes.Core.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Notes.WebApi.Modules
{
    public abstract class NotesModuleBase : NancyModule
    {
        protected NotesModuleBase(string path) : base(path)
        {
        }

        [Inject]
        public IHandlerFactory QueryHandlerFactory { get; set; }

        protected async Task<object> HandleQuery<TQuery>(TQuery query)
        {
            var handler = QueryHandlerFactory.CreateQueryHandler<TQuery>();

            var result = await handler.HandleAsync(query);

            return result;
        }

        protected async Task<object> HandleCommand<TCommand>(TCommand command)
        {
            try
            {
                var handler = QueryHandlerFactory.CreateCommandHandler<TCommand>();

                var result = await handler.HandleAsync(command);

                return result;
            }catch(Exception ex)
            {
                throw;
            }
        }
    }
}