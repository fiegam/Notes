using Nancy;
using Nancy.Testing;
using Notes.WebApi.AppStart;
using NUnit.Framework;
using System.Threading.Tasks;
using Ninject;
using Notes.Core.Repositories;
using Notes.WebApi.Tests.Fakes;
using System;

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

        public async Task<TResult> Get<TResult>(string path)
        {
            var response = await Browser.Get(path, with => with.Header("Accept", "application/json"));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            if(typeof(TResult) == typeof(string))
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
                with.Header("Accept", "application/json");
                with.Header("Content-type", "application/json");
                with.JsonBody(command);
            });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        public async Task Delete(string path)
        {
            var response = await Browser.Delete(path);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}