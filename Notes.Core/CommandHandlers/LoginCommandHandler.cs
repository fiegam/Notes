using Notes.Contract.Commands;
using Notes.Core.Configuration;
using Notes.Core.Model;
using Notes.Core.Repositories;
using Notes.Core.Servants;
using System;
using System.Threading.Tasks;

namespace Notes.Core.CommandHandlers
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand>
    {

        private const string UnauthorizedErrorMessage = "Requested credentials are not valid";
        private IAuthTokenServant _jwtTokenServant;
        private IAccountRepository _accountRepository;
        private IHashingServant _hashingServant;
        private IConfiguration _configuration;

        public LoginCommandHandler(IAccountRepository accountRepository, IHashingServant hashingServant, IAuthTokenServant jwtTokenServant, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _hashingServant = hashingServant;
            _jwtTokenServant = jwtTokenServant;
            _configuration = configuration;
        }

        public async Task<object> HandleAsync(LoginCommand command)
        {
            var account = await _accountRepository.GetAccount(command.AccountName);

            AssertCredentialsAreValid(command, account);

            var payload = new TokenPayload()
            {
                AccountId = account.Id,
                AccountName = command.AccountName,
                IssuedAt = DateTime.UtcNow,
                ValidUntil = DateTime.UtcNow.Add(_configuration.AuthTokenLifetime)
            };

            return new LoginCommandResult()
            {
                Token = _jwtTokenServant.Encode(payload),
                AccountName = payload.AccountName,
                IssuedAt = payload.IssuedAt,
                ValidUntil = payload.ValidUntil,
            };
        }

        private void AssertCredentialsAreValid(LoginCommand command, Account account )
        {
            if (account == null)
            {
                Unauthorized();
            }

            var hash = _hashingServant.CreatePasswordHash(command.Password, account.PasswordSalt);

            if (hash != account.PasswordHash)
            {
                Unauthorized();
            }
        }


        private void Unauthorized()
        {
            throw new UnauthorizedAccessException(UnauthorizedErrorMessage);
        }
    }
}