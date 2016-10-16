using Nancy;
using Nancy.Testing;
using Notes.Contract.Commands;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Notes.WebApi.Tests.Infrastructure
{
    public class NancyModuleTestsBase
    {
        protected Browser Browser { get; private set; }

        [SetUp]
        public void Setup()
        {
            var bootstrapper = new NotesTestsBootstrapper();
            Browser = new Browser(bootstrapper);
        }

        protected async Task LoginTest()
        {
            await Login("test", "test");
        }

        protected async Task Login(string accountName, string password)
        {
            var loginResult = await Post<LoginCommand, LoginCommandResult>("account/login", new LoginCommand
            {
                AccountName = accountName,
                Password = password
            });

            _authToken = loginResult.Token;
        }

        private string _authToken;

        private void AddStandardHeaders(BrowserContext context)
        {
            context.Header("Accept", "application/json");
            context.Header("Content-type", "application/json");
            if (!string.IsNullOrEmpty(_authToken))
            {
                context.Header("Authorization", _authToken);
            }
        }

        public async Task<TResult> Get<TResult>(string path)
        {
            var response = await Browser.Get(path, AddStandardHeaders);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            if (typeof(TResult) == typeof(string))
            {
                return (TResult)(object)response.Body.AsString();
            }

            var body = response.Body.DeserializeJson<TResult>();

            return body;
        }

        public async Task Put<TCommandType>(string path, TCommandType command)
        {
            var response = await Browser.Put(path, with =>
            {
                AddStandardHeaders(with);
                with.JsonBody(command);
            });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        public async Task<TCommandResult> Post<TCommandType, TCommandResult>(string path, TCommandType command)
        {
            var response = await Browser.Post(path, with =>
            {
                AddStandardHeaders(with);
                with.JsonBody(command);
            });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            return response.Body.DeserializeJson<TCommandResult>();
        }

        public async Task Delete(string path)
        {
            var response = await Browser.Delete(path,AddStandardHeaders);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}