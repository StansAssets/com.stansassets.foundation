using System;
using NUnit.Framework;
using StansAssets.Foundation.Patterns;

namespace StansAssets.Foundation.Tests.Patterns
{
    class TestSingleton : MonoSingleton<TestSingleton>
    {
        public readonly string Id;

        public TestSingleton()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    public class MonoSingletonTests
    {
        [Test]
        public void SingleInstanceTest()
        {
            var a = TestSingleton.Instance;
            var b = TestSingleton.Instance;
            var c = TestSingleton.Instance;
            Assert.IsTrue(a.Id.Equals(b.Id) && b.Id.Equals(c.Id));
        }
    }
}
