using System;
using NUnit.Framework;
using NUnit.Framework.Api;

namespace ArcoMage.Tests
{
    [TestFixture]
    class ResourceTests
    {
        private Resource resource;
        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(-1, 0)]
        [TestCase(-2, 2)]
        public void ResourceCorrectInit(int step, int count)
        {
            resource = new Resource(step, count);
            Assert.AreEqual(count, resource.Count);
            Assert.AreEqual(resource.Source, step);
        }

        [TestCase(-1,-1)]
        [TestCase(123,-123)]
        public void ResourceInCorrectInit(int step, int count)
        {
            Exception ex = null;
            try
            {
                resource = new Resource(step, count);
            }
            catch (Exception e)
            {
                ex = e;
            }
            Assert.NotNull(ex);
        }

        [TestCase(0, 5, 5, 25)]
        [TestCase(25, -5, 6, 0)]
        public void UpdateTest(int startCount, int step, int iterations, int expected)
        {
            resource = new Resource(step, startCount);
            for (var i = 0; i < iterations; i++)
                resource.Update();
            Assert.AreEqual(step, resource.Source);
            Assert.AreEqual(expected, resource.Count);
        }

        [Test]
        public void TakeDamageTest()
        {
            resource = new Resource(5,5);
            resource.TakeDamage(5,5);
            Assert.AreEqual(0, resource.Source);
            Assert.AreEqual(0, resource.Count);
            resource.TakeDamage(5,5);
            Assert.AreEqual(-5, resource.Source);
            Assert.AreEqual(0, resource.Count);
        }
    }
}
