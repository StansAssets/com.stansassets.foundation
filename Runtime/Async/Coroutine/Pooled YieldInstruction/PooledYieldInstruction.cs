using UnityEngine;

namespace StansAssets.Foundation.Async
{
    public class PooledYieldInstruction : CustomYieldInstruction 
    {
        public override bool keepWaiting{ get;}
    }
}