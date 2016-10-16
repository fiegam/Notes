namespace Notes.Core.Infrastructure
{
    public interface IIdentityProvider
    {
        NotesIdentity CurrentIdentity { get; }
    }
}