using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace StansAssets.Foundation.Patterns
{
    interface IStackProxy<T>
    {
        int Count { get; }
        void Clear();
        bool Contains(T item);
        void Push(T item);
        bool TryPop(out T result);
    }


    class StackProxy<T> : IStackProxy<T>
    {
        readonly Stack<T> m_Stack;

        public StackProxy() => m_Stack = new Stack<T>();
        public StackProxy(int defaultCapacity) =>  m_Stack = new Stack<T>(defaultCapacity);

        public int Count => m_Stack.Count;
        public void Clear() => m_Stack.Clear();
        public bool Contains(T item) => m_Stack.Contains(item);
        public void Push(T item) => m_Stack.Push(item);

        public bool TryPop(out T result)
        {
            if (m_Stack.Count == 0)
            {
                result = default;
                return false;
            }

            result = m_Stack.Pop();
            return true;
        }
    }

    class ConcurrentStackProxy<T> : IStackProxy<T>
    {
        readonly ConcurrentStack<T> m_Stack;

        public ConcurrentStackProxy()
        {
            m_Stack = new ConcurrentStack<T>();
        }

        public int Count => m_Stack.Count;
        public void Clear() => m_Stack.Clear();
        public bool Contains(T item) => m_Stack.Contains(item);
        public void Push(T item) => m_Stack.Push(item);
        public bool TryPop(out T result) => m_Stack.TryPop(out result);

    }
}
