## Event Bus

This is a well-known pattern that can be very handy in-game and app development. The main reason we should use `EventBus` is loose coupling. Sometimes, you want to process specific events that are interested in multiple parts of your application, like the presentation layer, business layer, and data layer, so EventBus provides an easy solution.

The package offers a very simple, light, and at the same time high-performant implementation of this pattern.

### Best Practices
Like any other pattern, it's very easy to misuse it or use it for not appropriate cases. Of course, it's up to you how to use it in own application, but here are few best practices when working with event buses.

* Consider using event bus when it’s difficult to couple the communicating components directly
* Avoid having components which are both publishers and subscribers. See `IReadOnlyEventBus`
* Avoid “events chains” (i.e. flows that involve multiple sequential events)
* Write tests to compensate for insufficient coupling and enforce inter-components integration

### Use Examples

#### Subscribe and Post

First of all you need to make a new `EventBus` instance:
```
var eventBus = new EventBus();
```

Before you can post any events make sure you declare few event classes to work with.
```
public class SampleEvent : IEvent
{
    public string Data { get; set; }
}

public class AnotherSampleEvent : IEvent
{
    public string Data { get; set; }
    public int IntData { get; set; }
}
```

Now you can subscribe to event:
```
eventBus.Subscribe<SampleEvent>((e) =>
{
    Debug.Log(e.Data);
});
```

And post an event:
```
var e = new SampleEvent { Data = "Hello World" };
eventBus.Post(e);
```

You may also use `Unsubscribe` method when you no longer need the subscription.

#### Static Bus
This is the simplest an fastest implementation for the event bus pattern.
Since this is static bus *DO NOT USE* it when you making a package, since it may conflict with user project.

It only make sense to use it inside the project you maintain and own.
Here is how the same subscribe & post flow will look like when using `StaticBus`:
```
var e = new SampleEvent { Data = "Hello World" };
StaticBus<SampleEvent>.Subscribe<SampleEvent>((e) =>
{
    Debug.Log(e.Data);
});


StaticBus<SampleEvent>.Post(e);
```

#### Using with pool
Another interesting example wold be to look at how you can combine events with pool:

```
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

    public static void Release(SamplePooledEvent e)
    {
        s_EventsPool.Release(e);
    }
}
```

Now here is how posting event would look like:
```
var e = SamplePooledEvent.GetPooled("Hello World");
StaticBus<SamplePooledEvent>.Post(e);
SamplePooledEvent.Release(e);
```

We can go further and add `IDisposable` wrapper around it:
```
public class SamplePooledEvent : IEvent
{
    static readonly DefaultPool<SamplePooledEvent> s_EventsPool = new DefaultPool<SamplePooledEvent>();
    public string Data { get; private set; }
    
    public static DefaultPool<SamplePooledEvent>.PooledObject GetPoolable(string data, out SamplePooledEvent e)
    {
        e = s_EventsPool.Get();
        e.Data = data;
        
        var poolable = new DefaultPool<SamplePooledEvent>.PooledObject(e, s_EventsPool);
        return poolable;
    }
}
```
Now it's even easier to post an event:
```
using (SamplePooledEvent.GetPoolable("Hello World", out var evt))
{
    StaticBus<SamplePooledEvent>.Post(e);
}
```
