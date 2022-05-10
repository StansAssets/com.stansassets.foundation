using System.Runtime.CompilerServices;
using UnityEngine;

namespace StansAssets.Foundation.Async
{
    public sealed class WaitForSecondsRealtimePooled : PooledYieldInstruction 
    {
        private float m_waitUntilTime = -1f;
        private float m_waitTime;
        
        public override bool keepWaiting 
        {
            get 
            {
                if (m_waitUntilTime < 0.0f)
                    m_waitUntilTime = Time.realtimeSinceStartup + m_waitTime;
                var waiting = Time.realtimeSinceStartup < m_waitUntilTime;
                if (!waiting)
                    Reset();
                return waiting;
            }
        }

        public override void Reset()
        {
            m_waitUntilTime = -1f;
            YieldPool.BackToPool(this);;
        }

        public WaitForSecondsRealtimePooled Wait(float time) 
        {
            m_waitTime = time;
            return this;
        }
    }
}