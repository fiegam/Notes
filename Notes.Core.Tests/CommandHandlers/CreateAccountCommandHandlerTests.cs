using Ninject;
using Notes.Contract.Commands;
using Notes.Core.CommandHandlers;
using Notes.Core.Exceptions;
using Notes.Core.Model;
using Notes.Core.Repositories;
using Notes.Core.Servants;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Notes.Core.Tests.CommandHandlers
{
    public class CreateAccountCommandHandlerTests : CommandHandlerTestsBase<CreateAccountCommandHandler>
    {
        [Test]
        public async Task CreatesAccount()
        {
            var command = new CreateAccountCommand
            {
                AccountName = "name",
                EmailAddress = "email",
                Password = "pass"
            };

            var salt = new byte[] { 10, 13 };
            string hash = "hash";
            _kernel.Get<IHashingServant>().CreateSalt().Returns(salt);
            _kernel.Get<IHashingServant>().CreatePasswordHash(command.Password, salt).Returns(hash);

            //act
            var account = await _sut.HandleAsync(command) as Contract.Model.Account;

            Assert.AreEqual(command.AccountName, account.Name);
            Assert.AreEqual(command.EmailAddress, account.EmailAddress);
            Assert.AreNotEqual(Guid.Empty, account.Id);

            await _kernel.Get<IAccountRepository>().Received().Add(
                Arg.Is<Account>(x => x.Name == command.AccountName &&
                x.EmailAddress == command.EmailAddress &&
                x.PasswordSalt == salt &&
                x.PasswordHash == hash));
        }

        [Test]
        public async Task DoNotCreateAccountIfAlreadyExists()
        {
            var command = new CreateAccountCommand
            {
                AccountName = "name",
                EmailAddress = "email",
                Password = "pass"
            };

            var salt = new byte[] { 10, 13 };
            string hash = "hash";
            _kernel.Get<IHashingServant>().CreateSalt().Returns(salt);
            _kernel.Get<IHashingServant>().CreatePasswordHash(command.Password, salt).Returns(hash);

            _kernel.Get<IAccountRepository>().GetAccount(command.AccountName).Returns(new Account() { Name = command.AccountName });

            //act
            Assert.ThrowsAsync<AccountlreadyExistsException>(async ()=> await _sut.HandleAsync(command));
            
            await _kernel.Get<IAccountRepository>().DidNotReceive().Add(
                Arg.Any<Account>());
        }
    }
}