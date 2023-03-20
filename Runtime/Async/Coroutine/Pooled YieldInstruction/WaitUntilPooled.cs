using System;

namespace StansAssets.Foundation.Async
{
    /// <summary>
    ///   <para>
    /// Custom Yield Instruction that suspends the coroutine execution until the supplied delegate evaluates to false,
    /// and can be pooled inside Yield Pool.
    /// </para>
    /// </summary>
    public sealed class WaitUntilPooled : PooledYieldInstruction
    {
        Func<bool> m_Predicate;
        bool m_Waiting;

        public override bool keepWaiting
        {
            get
            {
                m_Waiting = !m_Predicate();
                if (!m_Waiting)
                    YieldPool.BackToPool(this);
                return m_Waiting;
            }
        }

        public WaitUntilPooled Wait(Func<bool> predicate)
        {
            m_Predicate = predicate;
            m_Waiting = false;
            return this;
        }
    }
}
