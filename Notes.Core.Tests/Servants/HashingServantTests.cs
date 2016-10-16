using Notes.Core.Servants;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Core.Tests.Servants
{
    [TestFixture]
    public class HashingServantTests
    {
        private HashingServant _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new HashingServant();

        }

        [Test]
        public void CreateSalt_CreatesSalt()
        {
            //Act
            var salt = _sut.CreateSalt();

            Assert.NotNull(salt);
            Assert.IsNotEmpty(salt);
        }

        [Test]
        public void CreatePasswordHash_CreatesHash()
        {
            var salt = _sut.CreateSalt();

            var password = "pass";

            //Act
            var hashed = _sut.CreatePasswordHash(password, salt);

            Assert.IsNotEmpty(hashed);
            Assert.AreNotEqual(password, hashed);
        }

        [Test]
        public void CreatePasswordHash_CreatesSameHashEachTime()
        {
            var salt = _sut.CreateSalt();

            var password = "pass";

            //Act
            var hashed = _sut.CreatePasswordHash(password, salt);
            var hashed2 = _sut.CreatePasswordHash(password, salt);

            Assert.AreEqual(hashed2, hashed);
        }


        [Test]
        public void CreatePasswordHash_CreatesDifferentHashForDifferentSalt()
        {
            var salt = _sut.CreateSalt();
            var salt2 = _sut.CreateSalt();

            var password = "pass";

            //Act
            var hashed = _sut.CreatePasswordHash(password, salt);
            var hashed2 = _sut.CreatePasswordHash(password, salt2);

            Assert.AreNotEqual(hashed2, hashed);
        }

        [Test]
        public void CreatePasswordHash_CreatesDifferentHashForDifferentPass()
        {
            var salt = _sut.CreateSalt();

            var password = "pass";
            var password2 = "pass2";

            //Act
            var hashed = _sut.CreatePasswordHash(password, salt);
            var hashed2 = _sut.CreatePasswordHash(password2, salt);

            Assert.AreNotEqual(hashed2, hashed);
        }

    }
}
