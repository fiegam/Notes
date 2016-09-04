using Ninject;
using Notes.Core.Repositories;
using Notes.WebApi.AppStart;
using Notes.WebApi.Tests.Fakes;

namespace Notes.WebApi.Tests.Infrastructure
{
    public class NotesTestsBootstrapper : NotesBootstrapper
    {
        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            existingContainer.Rebind<INotesRepository>().ToConstant(NotesRepositoryFake.Instance);
        }
    }
}