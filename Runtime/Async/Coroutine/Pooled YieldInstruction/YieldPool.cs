using System;
using System.Collections.Generic;
using StansAssets.Foundation.Patterns;

namespace StansAssets.Foundation.Async
{
    public static class YieldPool
    {
        const int k_DefaultPoolSize = 10;
        static readonly Dictionary<Type, ObjectPool<PooledYieldInstruction>> s_Instructions;

        static YieldPool()
        {
            s_Instructions = new Dictionary<Type, ObjectPool<PooledYieldInstruction>>();
            
            Add<WaitUntilPooled>();
            Add<WaitWhilePooled>();
#if UNITY_2020_1_OR_NEWER
            Add<WaitForSecondsPooled>();
            Add<WaitForSecondsRealtimePooled>();
#endif
        }

        static void Add<T>() where T : PooledYieldInstruction, new()
        {
            s_Instructions.Add(typeof(T), new ObjectPool<PooledYieldInstruction>(() => new T()));
            for (var i = 0; i < k_DefaultPoolSize; i++) s_Instructions[typeof(T)].Release(new T());
        }

        internal static void BackToPool<T>(T pooled) where T : PooledYieldInstruction
        {
            s_Instructions[typeof(T)].Release(pooled);
        }

        static T GetFromPool<T>() where T : PooledYieldInstruction, new()
        {
            return (T)s_Instructions[typeof(T)].Get();
        }

#if UNITY_2020_1_OR_NEWER
        /// <summary>
        ///     Wait for a given number of seconds using scaled time.
        ///     <param name="seconds">Delay execution by the amount of time in seconds.</param>
        /// </summary>
        public static WaitForSecondsPooled WaitForSeconds(float seconds)
        {
            return GetFromPool<WaitForSecondsPooled>().Wait(seconds);
        }

        /// <summary>
        ///     Wait for a given number of seconds using realtime.
        ///     <param name="seconds">Delay execution by the amount of time in seconds.</param>
        /// </summary>
        public static WaitForSecondsRealtimePooled WaitForSecondsRealtime(float seconds)
        {
            return GetFromPool<WaitForSecondsRealtimePooled>().Wait(seconds);
        }
#endif

        /// <summary>
        ///     Suspends the coroutine execution until the supplied delegate evaluates to false.
        /// </summary>
        public static WaitUntilPooled WaitUntil(Func<bool> predicate)
        {
            return GetFromPool<WaitUntilPooled>().Wait(predicate);
        }

        /// <summary>
        ///     Suspends the coroutine execution until the supplied delegate evaluates to true.
        /// </summary>
        public static WaitWhilePooled WaitWhile(Func<bool> predicate)
        {
            return GetFromPool<WaitWhilePooled>().Wait(predicate);
        }
    }
}
