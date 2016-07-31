using Ninject;
using Notes.Core.QueryHandlers;
using System;
using Notes.Core.CommandHandlers;

namespace Notes.Core.Infrastructure
{
    public class QueryHandlerFactory : IHandlerFactory
    {
        private IKernel _kernel;

        public QueryHandlerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public ICommandHandler<TCommand> CreateCommandHandler<TCommand>()
        {
            var handlerType = typeof(ICommandHandler<TCommand>);
            return _kernel.Get(handlerType) as ICommandHandler<TCommand>;
        }

        public IQueryHandler<TQuery> CreateQueryHandler<TQuery>()
        {
            var handlerType = typeof(IQueryHandler<TQuery>);
            return _kernel.Get(handlerType) as IQueryHandler<TQuery>;
        }
    }
}