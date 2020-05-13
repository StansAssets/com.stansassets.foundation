using System;
using NUnit.Framework;
using UnityEngine;

namespace StansAssets.Foundation.Patterns.EditorTests
{
    public class TestClassObject
    {
        public int IntValue { get; } = 1;
        public string StringValue  { get; } = string.Empty;
    }

    [TestFixture(typeof(TestClassObject))]
    [TestFixture(typeof(Mesh))]
    public class ObjectPoolTests<T> where T : class, new()
    {
        [Test]
        public void NullCreateFactoryThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var unused = new ObjectPool<T>(null);
            });
        }

        [Test]
        public void InvalidLimitThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var unused = new ObjectPool<T>(() => new T(), maxSize: 0);
            });
        }

        [Test]
        public void ConcurrentModeWithDefaultCapacityThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var unused = new ObjectPool<T>(() => new T(), concurrent:true, defaultCapacity: 20);
            });
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Actions_AreCalledWhenGettingAndReleasing(bool isConcurrent)
        {
            var onGetCalled = false;
            var onReleaseCalled = false;

            var pool = new ObjectPool<T>(() => new T(), T => onGetCalled = true, T => onReleaseCalled = true, concurrent:isConcurrent);
            var instance = pool.Get();
            Assert.NotNull(instance);

            Assert.True(onGetCalled, "Expected OnGet action to be called but it was not");
            Assert.False(onReleaseCalled, "Expected onRelease action to not be called but it was.");

            onGetCalled = false;
            onReleaseCalled = false;

            pool.Release(instance);

            Assert.False(onGetCalled, "Expected OnGet action to not be called when calling Release.");
            Assert.True(onReleaseCalled, "Expected onRelease action to be called when calling Release.");
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void CountValuesAreCorrect(bool isConcurrent)
        {
            var pool = new ObjectPool<T>(() => new T(), concurrent: isConcurrent);

            Assert.AreEqual(0, pool.CountAll, "Expected CountAll to be 0 when first created");
            Assert.AreEqual(0, pool.CountInactive, "Expected CountInactive to be 0 when first created");
            Assert.AreEqual(0, pool.CountActive, "Expected CountActive to be 0 when first created");

            var instance1 = pool.Get();
            var instance2 = pool.Get();
            pool.Get();

            Assert.AreEqual(3, pool.CountAll, "Expected CountAll to be 3 when Get is called 3 times.");
            Assert.AreEqual(0, pool.CountInactive, "Expected CountInactive to be 0 when Get is called 3 times.");
            Assert.AreEqual(3, pool.CountActive, "Expected CountActive to be 3 when Get is called 3 times.");

            pool.Release(instance1);
            pool.Release(instance2);

            Assert.AreEqual(3, pool.CountAll, "Expected CountAll to be 3 when 2 instances are released back.");
            Assert.AreEqual(2,  pool.CountInactive, "Expected CountInactive to be 2 when 2 instances are released back.");
            Assert.AreEqual(1, pool.CountActive, "Expected CountActive to be 1 when 2 instances are released back.");
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void PreWarmTest(bool isConcurrent)
        {
            var getCallsCount = 0;
            var releaseCallsCount = 0;
            var pool = new ObjectPool<T>(
                () => new T(),
                T => { getCallsCount++; },
                T => { releaseCallsCount++;}
            ,isConcurrent);

            Assert.AreEqual(0, pool.CountAll);
            Assert.AreEqual(0, pool.CountInactive);
            Assert.AreEqual(0, pool.CountActive);

            const int preWarmCount = 10;
            pool.PreWarm(preWarmCount);
            Assert.AreEqual(preWarmCount, pool.CountAll);
            Assert.AreEqual(preWarmCount, pool.CountInactive);
            Assert.AreEqual(0, pool.CountActive);

            Assert.AreEqual(0, getCallsCount);
            Assert.AreEqual(preWarmCount, releaseCallsCount);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void LimitIsApplied(bool isConcurrent)
        {
            const int limit = 10;
            const int createCount = 15;

            var instances = new T[createCount];
            var pool = new ObjectPool<T>(() => new T(), null, null, true, isConcurrent, maxSize: limit);

            for(var i = 0; i < createCount; ++i)
                instances[i] = pool.Get();

            for(var i = createCount - 1; i >= 0; --i)
                pool.Release(instances[i]);

            Assert.AreEqual(pool.CountInactive, limit, "Expected the inactive count to within the limit");
        }
    }
}