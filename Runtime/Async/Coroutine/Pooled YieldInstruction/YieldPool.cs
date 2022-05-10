using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using StansAssets.Foundation.Patterns;

namespace StansAssets.Foundation.Async
{
    public static class YieldPool 
    {
        private static Dictionary<Type, DefaultPool<PooledYieldInstruction>> m_instructions;
        private const int k_defaultPoolSize = 10;
        static YieldPool() 
        {
            m_instructions = new Dictionary<Type, DefaultPool<PooledYieldInstruction>>();
            Add<WaitForSecondsPooled>();
            Add<WaitUntilPooled>();
            Add<WaitWhilePooled>();
            Add<WaitForSecondsRealtimePooled>();
        }

        private static void Add<T>() where T: PooledYieldInstruction, new() 
        {
            m_instructions.Add(typeof(T), new DefaultPool<PooledYieldInstruction>());
            for (var i = 0; i < k_defaultPoolSize; i++)
            {
                m_instructions[typeof(T)].Release(new T());
            }
        }

        internal static void BackToPool<T>(T pooled) where T : PooledYieldInstruction 
        {
            m_instructions[typeof(T)].Release(pooled);
        }

        private static T GetFromPool<T>() where T: PooledYieldInstruction, new() 
        {
            return (T) m_instructions[typeof(T)].Get();
        }
        /// <summary>
        ///  Wait for a given number of seconds using scaled time.
        /// <param name="seconds">Delay execution by the amount of time in seconds.</param>
        /// </summary>    
        public static WaitForSecondsPooled WaitForSeconds(float seconds) 
        {
            return GetFromPool<WaitForSecondsPooled>().Wait(seconds);
        }
        /// <summary>
        ///  Wait for a given number of seconds using realtime.
        /// <param name="seconds">Delay execution by the amount of time in seconds.</param>
        /// </summary> 
        public static WaitForSecondsRealtimePooled WaitForSecondsRealtime(float seconds) 
        {
            return GetFromPool<WaitForSecondsRealtimePooled>().Wait(seconds);
        }
        /// <summary>
        ///  Suspends the coroutine execution until the supplied delegate evaluates to false.
        /// </summary> 
        public static WaitUntilPooled WaitUntil(Func<bool> predicate) 
        {
            return GetFromPool<WaitUntilPooled>().Wait(predicate);
        }
        /// <summary>
        ///  Suspends the coroutine execution until the supplied delegate evaluates to true.
        /// </summary> 
        public static WaitWhilePooled WaitWhile(Func<bool> predicate) 
        {
            return GetFromPool<WaitWhilePooled>().Wait(predicate);
        }
    }
}
