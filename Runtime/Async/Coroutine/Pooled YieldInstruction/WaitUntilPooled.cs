using System.Runtime.CompilerServices;
using System;

namespace StansAssets.Foundation.Async {
    public sealed class WaitUntilPooled : PooledYieldInstruction
    {
        private Func<bool> predicate;
        private bool waiting;
        public override bool keepWaiting {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get {
                waiting = !predicate();
                if(!waiting)
                    YieldPool.BackToPool(this);
                return waiting;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WaitUntilPooled Wait(Func<bool> predicate){
            this.predicate = predicate;
            waiting = false;
            return this;
        }
    }
}