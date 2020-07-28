namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// Singleton pattern implementation.
    /// </summary>
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        static T s_Instance;

        /// <summary>
        /// Returns a singleton class instance.
        /// </summary>
        public static T Instance => s_Instance ?? (s_Instance = new T());
    }
}