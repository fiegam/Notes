using Nancy;
using Notes.WebApi.Tests.Infrastructure;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Notes.WebApi.Tests.Modules
{
    [TestFixture]
    public class InfoModuleTests : NancyModuleTestsBase
    {
        [Test]
        public async Task TestInfo()
        {
            // Act
            var result = await Get<string>("/");
            
            Assert.AreEqual("Working", result);
        }
    }
}