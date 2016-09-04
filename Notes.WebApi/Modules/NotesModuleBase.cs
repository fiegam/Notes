using Nancy;
using Nancy.Validation;
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
            var validationResult = this.Validate(query);

            if (!validationResult.IsValid)
            {
                return Negotiate.WithModel(validationResult).WithStatusCode(HttpStatusCode.BadRequest);
            }

            var handler = QueryHandlerFactory.CreateQueryHandler<TQuery>();

            var result = await handler.HandleAsync(query);

            return result;
        }

        protected async Task<object> HandleCommand<TCommand>(TCommand command)
        {
            try
            {
                var validationResult = this.Validate(command);

                if (!validationResult.IsValid)
                {
                    return Negotiate.WithModel(validationResult).WithStatusCode(HttpStatusCode.BadRequest);
                }
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