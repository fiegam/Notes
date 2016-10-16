using Ninject;

namespace Notes.Core.Infrastructure
{
    public abstract class HandlerBase : IInitializable
    {
        [Inject]
        public IIdentityProvider IdentityProvider { get; set; }

        public NotesIdentity CurrentIdentity { get; private set; }

        public void Initialize()
        {
            Assert.NotNull(IdentityProvider);

            CurrentIdentity = IdentityProvider.CurrentIdentity;
        }
    }
}