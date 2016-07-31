using System.Configuration;

namespace Notes.Core.Configuration
{
    public class Configuration : IConfiguration
    {
        public string MongoConnectionString { get; } = ConfigurationManager.AppSettings["mongo:connectionString"];
        public string MongoDatabase { get; } = ConfigurationManager.AppSettings["mongo:database"];
    }
}