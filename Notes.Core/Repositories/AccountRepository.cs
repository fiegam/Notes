using MongoDB.Driver;
using Notes.Core.Configuration;
using Notes.Core.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Core.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private MongoClient _client;
        private IMongoDatabase _database;

        public AccountRepository(IConfiguration configuration)
        {
            _client = new MongoClient(configuration.MongoConnectionString);
            _database = _client.GetDatabase(configuration.MongoDatabase);
        }

        private const string AccountCollectionName = "accounts";

        private IMongoCollection<Account> AccountsCollection { get { return _database.GetCollection<Account>(AccountCollectionName); } }

        private FilterDefinition<Account> AccountByNameFilter(string name)
        {
            return Builders<Account>.Filter.Eq(x => x.Name, name);
        }

        public async Task<Account> GetAccount(string name)
        {
            var accounts = await AccountsCollection.FindAsync(AccountByNameFilter(name));
            return accounts.FirstOrDefault();
        }

        public async Task Add(Account account)
        {
            await AccountsCollection.InsertOneAsync(account);
        }
    }
}