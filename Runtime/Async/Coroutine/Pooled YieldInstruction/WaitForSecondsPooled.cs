using UnityEngine;

namespace StansAssets.Foundation.Async
{
    /// <summary>
    ///   <para>Custom Yield Instruction that waits for a given number of seconds using scaled time and can be pooled inside Yield Pool.</para>
    /// </summary>
    public class WaitForSecondsPooled : PooledYieldInstruction
    {
        bool m_Waiting;
        float m_WaitTime;

        public override bool keepWaiting
        {
            get
            {
                m_Waiting = m_WaitTime > 0f;
                if (m_Waiting)
                    m_WaitTime -= Time.deltaTime;
                else
                    Reset();
                return m_Waiting;
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