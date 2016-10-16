namespace Notes.Core.Servants
{
    public interface IHashingServant
    {
        string CreatePasswordHash(string pwd, byte[] salt);
        byte[] CreateSalt();
    }
}