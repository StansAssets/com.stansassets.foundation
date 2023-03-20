using UnityEngine;

namespace StansAssets.Foundation.Async
{
    /// <summary>
    ///   <para>Custom Yield Instruction that waits for a given number of seconds using realtime and can be pooled inside Yield Pool.</para>
    /// </summary>
    public sealed class WaitForSecondsRealtimePooled : PooledYieldInstruction
    {
        float m_WaitTime;
        float m_WaitUntilTime = -1f;

        public override bool keepWaiting
        {
            get
            {
                if (m_WaitUntilTime < 0.0f)
                    m_WaitUntilTime = Time.realtimeSinceStartup + m_WaitTime;
                var waiting = Time.realtimeSinceStartup < m_WaitUntilTime;
                if (!waiting)
                    Reset();
                return waiting;
            }
        }

#if UNITY_2020_1_OR_NEWER
        public override void Reset()
#else
        public new void Reset()
#endif
        {
            m_WaitUntilTime = -1f;
            YieldPool.BackToPool(this);
        }
        
        public WaitForSecondsRealtimePooled Wait(float time)
        {
            m_WaitTime = time;
            return this;
        }
    }
}