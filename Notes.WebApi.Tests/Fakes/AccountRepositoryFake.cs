using Notes.Core.Model;
using Notes.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.WebApi.Tests.Fakes
{
    public class AccountRepositoryFake : IAccountRepository
    {
        public static Account TestAccount { get; } = new Account
        {
            Id = Guid.Parse("f8e9558a-8e71-4b18-b811-66f305e73f15"),
            Name = "test",
            PasswordHash = "NbzIhoY6uHxzZtwiITxb0Xx2Q7T41EBkzgHGARh1Uww=",
            PasswordSalt = Convert.FromBase64String("4rezoh7F4OCT3omlsfs/6E+ETghY/+O70qAmLGeNSQsRPvXoPzDIjq2GULfrTgQaVwX5Yb+FhAC983INzPhpPPYxdVsci+5/AqNTsam4PsLdfznZis6F+e2awDYbvQNuiTh5pMc7gJvarZfGaw+w7aWJ/SW/04x4m0jbh2Az4vsNpq+2g13E3DG4G2+K2+mOPlZsGuOHPlRIvhYi8JxywLvZh9LYnsIwpe7pw/EVPFIvltOIR0gV/o7RvsobRtkX4eCAUybex4f8SLBFIZD2G66xnslb+wpPr99gf+SsplP2Kd9eKjHhbtD+IzGDSbYyWBERGun2fwHBtZXqpUhOdA==")
        };

        public static AccountRepositoryFake Instance = new AccountRepositoryFake();

        

    private AccountRepositoryFake()
        {
            Accounts.Add(TestAccount);
        }

        public List<Account> Accounts { get; } = new List<Account>();

        public async Task<Account> GetAccount(string name)
        {
            return await Task.Run(() => Accounts.FirstOrDefault(x => x.Name == name));
        }

        public async Task Add(Account account)
        {
            await Task.Run(() => Accounts.Add(account));
        }
    }
}