using Nancy;
using Ninject;
using Notes.Core.Repositories;
using Notes.WebApi.AppStart;
using Notes.WebApi.Tests.Fakes;

namespace Notes.WebApi.Tests.Infrastructure
{
    public class NotesTestsBootstrapper : NotesBootstrapper
    {
       
        protected override void ConfigureRequestContainer(IKernel container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            container.Rebind<INotesRepository>().ToConstant(NotesRepositoryFake.Instance);
            container.Rebind<IAccountRepository>().ToConstant(AccountRepositoryFake.Instance);
        }
    }
}