namespace StansAssets.Foundation.Async
{
    class MainThreadDispatcherRuntime : MainThreadDispatcherBase
    {
        public override void Init()
        {
            MonoBehaviourCallback.Instantiate();
            MonoBehaviourCallback.OnUpdate += Update;
        }
    }
}
