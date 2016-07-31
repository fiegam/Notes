namespace Notes.Core.Configuration
{
    public interface IConfiguration
    {
        string MongoConnectionString { get; }
        string MongoDatabase { get; }
    }
}