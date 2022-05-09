using System.Runtime.CompilerServices;
using UnityEngine;

namespace StansAssets.Foundation.Async{
    public sealed class WaitForSecondsRealtimePooled : PooledYieldInstruction {
        private float waitUntilTime = -1f;
        private float waitTime { get; set; }
        
        public override bool keepWaiting {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get {
                if (waitUntilTime < 0.0f)
                    waitUntilTime = Time.realtimeSinceStartup + waitTime;
                var waiting = Time.realtimeSinceStartup < waitUntilTime;
                if (!waiting)
                    Reset();
                return waiting;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Reset(){
            waitUntilTime = -1f;
            YieldPool.BackToPool(this);;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WaitForSecondsRealtimePooled Wait(float time) {
            waitTime = time;
            return this;
        }
    }
}