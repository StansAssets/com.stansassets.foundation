public sealed class WaitForEndOfFramePooled : PooledYieldInstruction
{
    public override bool keepWaiting => false;
}