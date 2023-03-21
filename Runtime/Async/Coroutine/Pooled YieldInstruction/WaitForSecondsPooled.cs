using UnityEngine;

namespace StansAssets.Foundation.Async
{
    /// <summary>
    /// Custom Yield Instruction that waits for a given number of seconds using scaled time and can be pooled inside Yield Pool.
    /// </summary>
    public class WaitForSecondsPooled : PooledYieldInstruction
    {
        float m_WaitTime;

        public override bool keepWaiting
        {
            get
            {
                var waiting = m_WaitTime > 0f;
                if (waiting)
                    m_WaitTime -= Time.deltaTime;
                else
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
            m_WaitTime = -1f;
            YieldPool.BackToPool(this);
        }
        
        public WaitForSecondsPooled Wait(float time)
        {
            m_WaitTime = time;
            return this;
        }
    }
}