using Nancy;
using Nancy.Security;
using Nancy.Validation;
using Ninject;
using Notes.Core.Infrastructure;
using Notes.WebApi.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Notes.WebApi.Modules
{
    public abstract class NotesModuleBase : NancyModule, IInitializable
    {
        private static ILogger Logger = LogProvider.GetLogger();
        private bool _allowAnonymous;

        protected NotesModuleBase(string path, bool allowAnonymous = false) : base(path)
        {
            _allowAnonymous = allowAnonymous;
            
        }

        [Inject]
        public IHandlerFactory HandlerFactory { get; set; }

        [Inject]
        public IAuthenticationProvider AuthenicationProvider { get; set; }

        protected async Task<object> HandleQuery<TQuery>(TQuery query)
        {
            var validationResult = this.Validate(query);

            if (!validationResult.IsValid)
            {
                return Negotiate.WithModel(validationResult).WithStatusCode(HttpStatusCode.BadRequest);
            }

            var handler = HandlerFactory.CreateQueryHandler<TQuery>();

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
                var handler = HandlerFactory.CreateCommandHandler<TCommand>();

                var result = await handler.HandleAsync(command);

                return result;
            }catch(Exception ex)
            {
                Logger.Error($"Exception when handling {typeof(TCommand)}", ex);
                throw;
            }
        }

        public void Initialize()
        {
            if(!_allowAnonymous)
            {
                AuthenicationProvider.Enable(this);
                this.RequiresAuthentication();
            }
        }
    }
}