using System;
using System.Configuration;

namespace Notes.Core.Configuration
{
    public class Configuration : IConfiguration
    {
        public TimeSpan AuthTokenLifetime { get; } = TimeSpan.Parse(ConfigurationManager.AppSettings["auth:tokenLifetime"]);
        public byte[] JwtKey { get; } = Convert.FromBase64String( ConfigurationManager.AppSettings["auth:jwtKey"]);

        public string MongoConnectionString { get; } = ConfigurationManager.AppSettings["mongo:connectionString"];
        public string MongoDatabase { get; } = ConfigurationManager.AppSettings["mongo:database"];
    }
}