using System.Runtime.CompilerServices;
using UnityEngine;

public class WaitForSecondsPooled : PooledYieldInstruction {
    private float waitTime;
    public override bool keepWaiting {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get {
            var waiting = waitTime > 0f;
            if(waiting)
                waitTime -= Time.deltaTime;
            else
                Reset();
            return waiting;
        }
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Reset(){
        waitTime = -1f;
        YieldPool.BackToPool(this);;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public WaitForSecondsPooled Wait(float time) {
        waitTime = time;
        return this;
    }
}