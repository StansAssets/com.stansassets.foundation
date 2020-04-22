# Pooling Pattern
This article demonstrates how to use pool pattern and how useful it could be. If you have ever written a poolable MTS/COM+ component, you can skip this funny intro. Otherwise keep reading... What is a pool? A container, full with water, where fish swim. In our case, an object pool is a container, where objects "swim" :) No, seriously, the object pool is a container, which not only allows objects to be drawn from, and returned back, but also creates them on the fly, whenever you want to draw more objects than you have at hand. When you need a new object, the pool searches for a free object to give you. If the "fish" is not found in the pool, the pool "gives birth" to a bunch of brand new objects and hands you one of them. If a free one was found, the pool just gives it to you. "But why", you'll wonder, "do I need an object pool that creates objects? Can't I just instantiate as many objects as I wish?". My answer is: Yes, you could. But not in all cases. There are some special cases, where to just pull the caught fish from the bucket is better (and definitely faster) then to catch a new fish.

## Reusing objects (not classes)
If all objects were small and fast, the programmer would die from happiness, that's why the world serves us heavy tasks which need heavy objects. And heavy not only means that an object has many data embedded in properties and data structures but also means heavy initialization code. Imagine you have a bunch of Dictionary objects, which are essentially the same but are used to translate different languages. Well, they could easily be written as a single class, which takes an argument -- the desired language. The object then, connects to a database, pulls N megabytes, stores it in some internal data structures, and is ready to be used. Now imagine that you should create a new object for every instance of its client. Well, I guess you don't want to waste a minute or so for each Dictionary creation, do you? So the problem is apparent, but the solution not yet. If there were some mechanism which allowed you to create the objects, store them anywhere, put them to sleep, and wake them only when you need them, you wouldn't have any problem, right? (Maybe.:) Right! Here's were object (not class) reuse come to help you. I've seen several implementations of object pooling, of which the best one is Microsoft's implementation of COM+ components pooling. I'll not compete with Microsoft (yet:), but will give the first (known to me) implementation of object pooling for .NET objects. So read along and enjoy...

## History (or about COM+ pooling)
(This subsection's text is copy/pasted from Platform SDK/Component Services/ Services Provided by COM+/Object Pooling and is copyrighted (c) material of Microsoft corp.)
Object pooling is an automatic service provided by COM+ that enables you to configure a component to have instances of itself kept active in a pool, ready to be used by any client that requests the component. You can administratively configure and monitor the pool maintained for a given component, specifying characteristics such as pool size and creation request time-out values. When the application is running, COM+ manages the pool for you, handling the details of object activation and reuse according to the criteria you have specified.
You can achieve very significant performance and scaling benefits by reusing objects in this manner, particularly when they are written to take full advantage of reuse. With object pooling, you gain the following benefits:

* You can speed object use time for each client, factoring out time-consuming initialization and resource acquisition from the actual work that the object performs for clients.
* You can share the cost of acquiring expensive resources across all clients.
* You can pre-allocate objects when the application starts, before any client requests come in.
* You can govern resource use with administrative pool management—for example, by setting an appropriate maximum pool level, you can keep open only as many database connections as you have a license for.
* You can administratively configure pooling to take the best advantage of available hardware resources—you can easily adjust the pool configuration as available hardware resources change.
* You can speed reactivation time for objects that use Just-in-Time (JIT) activation, while deliberately controlling how resources are dedicated to clients.

## Pooling with `ObjectPool`
* speed object use time...
* share the cost of acquiring expensive resources...
* pre-allocate objects when the application starts...
* construct (configure) objects, like COM+ (not mentioned above)

