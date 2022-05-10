using System.Runtime.CompilerServices;
using System;

namespace StansAssets.Foundation.Async 
{
    public sealed class WaitWhilePooled : PooledYieldInstruction
    {
        private Func<bool> m_predicate;
        private bool m_waiting;
        public override bool keepWaiting 
        { 
            get
            {
                m_waiting = m_predicate();
                if(!m_waiting)
                    YieldPool.BackToPool(this);
                return m_waiting; 
            }
        }

        public WaitWhilePooled Wait(Func<bool> predicate)
        {
            this.m_predicate = predicate;
            m_waiting = false;
            return this;
        }
    }
}