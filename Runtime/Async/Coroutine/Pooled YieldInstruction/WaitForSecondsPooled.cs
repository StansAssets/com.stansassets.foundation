using UnityEngine;

namespace StansAssets.Foundation.Async
{
    public class WaitForSecondsPooled : PooledYieldInstruction
    {
        bool m_waiting;
        float m_waitTime;

        public override bool keepWaiting
        {
            get
            {
                m_waiting = m_waitTime > 0f;
                if (m_waiting)
                    m_waitTime -= Time.deltaTime;
                else
                    Reset();
                return m_waiting;
            }
        }

#if UNITY_2020_1_OR_NEWER
        public override void Reset()
#else
        public new void Reset()
#endif      
        {
            m_waitTime = -1f;
            YieldPool.BackToPool(this);
        }
        
        public WaitForSecondsPooled Wait(float time)
        {
            m_waitTime = time;
            return this;
        }
    }
}