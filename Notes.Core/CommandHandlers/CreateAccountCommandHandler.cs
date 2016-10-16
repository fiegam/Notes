using Notes.Contract.Commands;
using Notes.Core.Exceptions;
using Notes.Core.Infrastructure.Extensions;
using Notes.Core.Model;
using Notes.Core.Repositories;
using Notes.Core.Servants;
using System;
using System.Threading.Tasks;

namespace Notes.Core.CommandHandlers
{
    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand>
    {
        private IAccountRepository _accountRepository;
        private IHashingServant _hashingServant;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IHashingServant hashingServant)
        {
            _accountRepository = accountRepository;
            _hashingServant = hashingServant;
        }

        public async Task<object> HandleAsync(CreateAccountCommand command)
        {
            await AssertAccountNotExists(command);

            var salt = _hashingServant.CreateSalt();
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Name = command.AccountName,
                EmailAddress = command.EmailAddress,
                PasswordSalt = salt,
                PasswordHash = _hashingServant.CreatePasswordHash(command.Password, salt)
            };

            await _accountRepository.Add(account);

            return account.MapTo<Contract.Model.Account>();
        }

        private async Task AssertAccountNotExists(CreateAccountCommand command)
        {
            var existing = await _accountRepository.GetAccount(command.AccountName);
            if (existing != null)
            {
                throw new AccountlreadyExistsException(command.AccountName);
            }
        }
    }
}