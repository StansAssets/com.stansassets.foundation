using NUnit.Framework;

namespace StansAssets.Foundation.Patterns.EditorTests
{
    public class EventBusTests
    {
        public class SamplePooledEvent : IEvent
        {
            static readonly DefaultPool<SamplePooledEvent> s_EventsPool = new DefaultPool<SamplePooledEvent>();
            public string Data { get; private set; }
            
            public static SamplePooledEvent GetPooled(string data)
            {
                var e = s_EventsPool.Get();
                e.Data = data;
                return e;
            }
            
            public static DefaultPool<SamplePooledEvent>.PooledObject GetPoolable(string data, out SamplePooledEvent e)
            {
                e = s_EventsPool.Get();
                e.Data = data;
                
                var poolable = new DefaultPool<SamplePooledEvent>.PooledObject(e, s_EventsPool);
                return poolable;
            }
            
            public static void Release(SamplePooledEvent e)
            {
                s_EventsPool.Release(e);
            }
        }


        public class SampleEvent : IEvent
        {
            public string Data { get; set; }
        }

        public class AnotherSampleEvent : IEvent
        {
            public string Data { get; set; }
            public int IntData { get; set; }
        }

        bool m_EventReceived;

        [SetUp]
        public void Setup()
        {
            m_EventReceived = false;
        }

        [Test]
        public void PostEventTest()
        {
            var eventBus = new EventBus();

            var e = new SampleEvent { Data = "Hello World" };
            var e2 = new AnotherSampleEvent { Data = "Hello World 2" };

            eventBus.Subscribe<SampleEvent>(OnSampleEvent);
            Assert.IsFalse(m_EventReceived);

            eventBus.Post(e);
            Assert.IsTrue(m_EventReceived);

            m_EventReceived = false;
            eventBus.Post(e2);
            Assert.IsFalse(m_EventReceived);

            eventBus.Unsubscribe<SampleEvent>(OnSampleEvent);
            eventBus.Post(e);
            Assert.IsFalse(m_EventReceived);
        }

        [Test]
        public void StaticBusTest()
        {
            var e = new SampleEvent { Data = "Hello World" };
            var e2 = new AnotherSampleEvent { Data = "Hello World 2" };

            StaticBus<SampleEvent>.Subscribe(OnSampleEvent);
            Assert.IsFalse(m_EventReceived);

            StaticBus<SampleEvent>.Post(e);
            Assert.IsTrue(m_EventReceived);

            m_EventReceived = false;
            StaticBus<AnotherSampleEvent>.Post(e2);
            Assert.IsFalse(m_EventReceived);

            StaticBus<SampleEvent>.Unsubscribe(OnSampleEvent);
            StaticBus<SampleEvent>.Post(e);
            Assert.IsFalse(m_EventReceived);
        }

        [Test]
        public void TestEventBusWithPoolableEvent()
        {
            var e = SamplePooledEvent.GetPooled("Hello World");
            StaticBus<SamplePooledEvent>.Subscribe(OnSampleEvent);
            StaticBus<SamplePooledEvent>.Post(e);
            SamplePooledEvent.Release(e);
            Assert.IsTrue(m_EventReceived);


            m_EventReceived = false;
            using (SamplePooledEvent.GetPoolable("Hello World", out var evt))
            {
                StaticBus<SamplePooledEvent>.Post(e);
            }
            Assert.IsTrue(m_EventReceived);
        }

        void OnSampleEvent(SamplePooledEvent e)
        {
            Assert.That(e.Data, Is.EqualTo("Hello World"));
            m_EventReceived = true;
        }
        
        
        void OnSampleEvent(SampleEvent e)
        {
            Assert.That(e.Data, Is.EqualTo("Hello World"));
            m_EventReceived = true;
        }
    }
}
