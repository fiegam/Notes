using Notes.Contract.Commands;
using Notes.Core.CommandHandlers;
using Notes.Core.Configuration;
using Notes.Core.Model;
using Notes.Core.Repositories;
using Notes.Core.Servants;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Notes.Core.Tests.CommandHandlers
{
    public class LoginCommandHandlerTests : CommandHandlerTestsBase<LoginCommandHandler>
    {
        protected override void TestSetup()
        {
            Service<IConfiguration>().AuthTokenLifetime.Returns(TimeSpan.FromHours(1));
        }

        [Test]
        public async Task GenetatesToken()
        {
            var command = new LoginCommand()
            {
                AccountName = "name",
                Password = "pass"
            };

            var account = new Account()
            {
                Name = command.AccountName,
                PasswordHash = "hash",
                PasswordSalt = new byte[] { 10, 11, 12 }
            };

            Service<IAccountRepository>().GetAccount(command.AccountName).Returns(account);

            Service<IHashingServant>().CreatePasswordHash(command.Password, account.PasswordSalt).Returns(account.PasswordHash);

            string token = "token";
            Service<IAuthTokenServant>().Encode(Arg.Any<TokenPayload>()).Returns(token);

            //act
            var result = await _sut.HandleAsync(command) as LoginCommandResult;

            Assert.NotNull(result);
            Assert.AreEqual(account.Name, result.AccountName);
            Assert.AreEqual(token, result.Token);

            Assert.LessOrEqual(result.IssuedAt, DateTime.UtcNow);
            Assert.Greater(result.ValidUntil, DateTime.UtcNow);
        }

        [Test]
        public void ThrowsUnauthorizedException_UnknownUser()
        {
            var command = new LoginCommand()
            {
                AccountName = "invalid",
                Password = "pass"
            };

            //act
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _sut.HandleAsync(command));
        }

        [Test]
        public void ThrowsUnauthorizedException_WrongPassword()
        {
            var command = new LoginCommand()
            {
                AccountName = "name",
                Password = "invalid"
            };

            var account = new Account()
            {
                Name = command.AccountName,
                PasswordHash = "hash",
                PasswordSalt = new byte[] { 10, 11, 12 }
            };

            Service<IAccountRepository>().GetAccount(command.AccountName).Returns(account);

            Service<IHashingServant>().CreatePasswordHash(command.Password, account.PasswordSalt).Returns("invalidHash");

            //act
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _sut.HandleAsync(command));
        }
    }
}