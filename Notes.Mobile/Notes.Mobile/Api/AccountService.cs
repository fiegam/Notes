using Notes.Contract.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Mobile.Api
{
    public interface IAccountService
    {
        Task<LoginCommandResult> Login(string accountName, string passowrd);
    }

    public class AccountService: ApiServiceBase, IAccountService
    {
        public async Task<LoginCommandResult> Login(string accountName, string passowrd)
        {
            var result = await Post<LoginCommand, LoginCommandResult>("Account/Login", new LoginCommand
            {
                AccountName = accountName,
                Password = passowrd
            });

            Authorize(result.Token);
            return result;
        }
    }
}
