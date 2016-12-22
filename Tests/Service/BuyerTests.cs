using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Service
{
    [TestFixture]
    class BuyerTests
    {
        [SetUp]
        public void Setup()
        {
            //before each every method it will be executed
        }

        [TearDown]
        public void Cleanup()
        {
            //at the end
        }

        [Test]
        public void DefaultCtorTest()
        {
            //var b = new Buyer();
            //Assert.AreEqual(0, p.Id);
            //Assert.AreEqual(0.0, p.Price);
            //Assert.AreEqual(null, p.Name);
            //Assert.AreEqual(null, p.Description);
            //Assert.AreEqual(null, p.Img);
            //Assert.AreEqual(0, p.CategoryId);
            //Assert.AreEqual(0, p.Number);
        }
    }
}
