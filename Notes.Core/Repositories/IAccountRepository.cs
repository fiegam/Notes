using System.Threading.Tasks;
using Notes.Core.Model;

namespace Notes.Core.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetAccount(string name);
        Task Add(Account account);
    }
}