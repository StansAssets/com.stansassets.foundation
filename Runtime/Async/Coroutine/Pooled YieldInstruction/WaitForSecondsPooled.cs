using System.Runtime.CompilerServices;
using UnityEngine;

namespace StansAssets.Foundation.Async 
{
    public class WaitForSecondsPooled : PooledYieldInstruction 
    {
        private float m_waitTime;
        private bool m_waiting;
        public override bool keepWaiting 
        {
            get 
            {
                m_waiting = m_waitTime > 0f;
                if(m_waiting)
                    m_waitTime -= Time.deltaTime;
                else
                    Reset();
                return m_waiting;
            }
        }

        public override void Reset()
        {
            m_waitTime = -1f;
            YieldPool.BackToPool(this);;
        }
 
        public WaitForSecondsPooled Wait(float time) 
        {
            m_waitTime = time;
            return this;
        }
    }
}