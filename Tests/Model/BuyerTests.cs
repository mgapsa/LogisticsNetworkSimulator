using DataModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Model
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
            var b = new Buyer();
            Assert.AreEqual(0, b.Id);
            Assert.AreEqual(b.OptionA, EnumTypes.BuyerAOptions.Static);
            Assert.AreEqual(b.Amount, 50);
            Assert.AreEqual(b.MinAmount, 25);
            Assert.AreEqual(b.MaxAmount, 75);
            Assert.AreEqual(b.Lambda, 50);
            Assert.AreEqual(b.MeanOptionA, 50);
            Assert.AreEqual(b.DeviationOptionA, 5);
            Assert.AreEqual(b.OptionB, EnumTypes.BuyerBOptions.Static);
            Assert.AreEqual(b.Minutes, 20);
            Assert.AreEqual(b.MeanOptionB, 50);
            Assert.AreEqual(b.DeviationOptionB, 10);
        }
    }
}
