using System;

namespace StansAssets.Foundation.Async
{
    /// <summary>
    /// Custom Yield Instruction that suspends the coroutine execution until the supplied delegate evaluates to true, and can be pooled inside Yield Pool.
    /// </summary>
    public sealed class WaitWhilePooled : PooledYieldInstruction
    {
        Func<bool> m_Predicate;

        public override bool keepWaiting
        {
            get
            {
                var waiting = m_Predicate();
                if (!waiting)
                    YieldPool.BackToPool(this);
                return waiting;
            }
        }

        public WaitWhilePooled Wait(Func<bool> predicate)
        {
            m_Predicate = predicate;
            return this;
        }
    }
}
