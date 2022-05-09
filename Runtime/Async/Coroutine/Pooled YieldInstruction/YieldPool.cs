using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;

namespace StansAssets.Foundation.Async{
    public static class YieldPool {
        private static Dictionary<int, Queue<PooledYieldInstruction>> instructions;
        private static readonly int cacheSize = 6;

        static YieldPool() {
            instructions = new Dictionary<int, Queue<PooledYieldInstruction>>();
            Add<WaitForSecondsPooled>(cacheSize);
            Add<WaitUntilPooled>(cacheSize);
            Add<WaitWhilePooled>(cacheSize);
            Add<WaitForSecondsRealtimePooled>(cacheSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Add<T>(int size) where T: PooledYieldInstruction, new() {
            instructions.Add(YieldType<T>.Index, new Queue<PooledYieldInstruction>());
            for (var i = 0; i < size; i++)
            {
                instructions[YieldType<T>.Index].Enqueue(new T());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void BackToPool<T>(T pooled) where T : PooledYieldInstruction {
            instructions[YieldType<T>.Index].Enqueue(pooled);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T GetFromPool<T>() where T: PooledYieldInstruction, new() {
            var queue = instructions[YieldType<T>.Index];
            if(queue.Count < 1)
                Add<T>(cacheSize);
            return (T) queue.Dequeue();
        }
        /// <summary>
        ///  Wait for a given number of seconds using scaled time.
        /// <param name="seconds">Delay execution by the amount of time in seconds.</param>
        /// </summary>    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WaitForSecondsPooled WaitForSeconds(float seconds) {
            return GetFromPool<WaitForSecondsPooled>().Wait(seconds);
        }
        /// <summary>
        ///  Wait for a given number of seconds using realtime.
        /// <param name="seconds">Delay execution by the amount of time in seconds.</param>
        /// </summary> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WaitForSecondsRealtimePooled WaitForSecondsRealtime(float seconds) {
            return GetFromPool<WaitForSecondsRealtimePooled>().Wait(seconds);
        }
        /// <summary>
        ///  Suspends the coroutine execution until the supplied delegate evaluates to false.
        /// </summary> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WaitUntilPooled WaitUntil(Func<bool> predicate) {
            return GetFromPool<WaitUntilPooled>().Wait(predicate);
        }
        /// <summary>
        ///  Suspends the coroutine execution until the supplied delegate evaluates to true.
        /// </summary> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WaitWhilePooled WaitWhile(Func<bool> predicate) {
            return GetFromPool<WaitWhilePooled>().Wait(predicate);
        }
    }
}
