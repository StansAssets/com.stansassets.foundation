## Event Bus

This is a well-known pattern that can be very handy in-game and app development. The main reason we should use `EventBus` is loose coupling. Sometimes, you want to process specific events that are interested in multiple parts of your application, like the presentation layer, business layer, and data layer, so EventBus provides an easy solution.

The package offers a very simple, light, and at the same time high-performant implementation of this pattern.

### Best Practices
Like any other pattern, it's very easy to misuse it or use it for not appropriate cases. Of course, it's up to you how to use it in own application, but here are few best practices when working with event buses.

* Consider using event bus when it’s difficult to couple the communicating components directly
* Avoid having components which are both publishers and subscribers. See `IReadOnlyEventBus`
* Avoid “events chains” (i.e. flows that involve multiple sequential events)
* Write tests to compensate for insufficient coupling and enforce inter-components integration

### Use Example
...coming when API is approved
