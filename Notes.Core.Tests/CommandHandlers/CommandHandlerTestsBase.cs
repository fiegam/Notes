using Ninject;
using Ninject.MockingKernel.NSubstitute;
using NUnit.Framework;

namespace Notes.Core.Tests.CommandHandlers
{
    [TestFixture]
    public class CommandHandlerTestsBase<TCommandHandler>
    {
        protected NSubstituteMockingKernel _kernel;
        protected TCommandHandler _sut;

        protected TService Service<TService>()
        {
            return _kernel.Get<TService>();
        }

        [SetUp]
        public void Setup()
        {
            _kernel = new NSubstituteMockingKernel();

            _sut = _kernel.Get<TCommandHandler>();

            TestSetup();
        }

        protected virtual void TestSetup()
        {

        }

        protected virtual void TestTearDown()
        {

        }

        [TearDown]
        public void TearDown()
        {
            TestTearDown();

            _kernel.Dispose();
            _kernel = null;
        }
    }
}