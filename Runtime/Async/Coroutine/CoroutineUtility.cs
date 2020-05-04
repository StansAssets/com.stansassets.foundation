using System;
using System.Collections;
using UnityEngine;

namespace StansAssets.Foundation.Async
{
    /// <summary>
    /// Coroutine helper methods and <see cref="MonoBehaviour"/> callbacks.
    /// </summary>
    public static class CoroutineUtility
    {
        /// <summary>
        /// Starts a Coroutine.
        /// An equivalent for [MonoBehaviour.StartCoroutine](https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html)
        ///
        /// The execution of a coroutine can be paused at any point using the yield statement.
        /// When a yield statement is used, the coroutine pauses execution and automatically resumes at the next frame.
        /// See the Coroutines documentation for more details.
        ///
        /// Coroutines are excellent when modeling behavior over several frames.
        /// The StartCoroutine method returns upon the first yield return, however you can yield the result,
        /// which waits until the coroutine has finished execution.
        /// There is no guarantee coroutines end in the same order they started, even if they finish in the same frame.
        ///
        /// Yielding of any type, including null, results in the execution coming back on a later frame, unless the coroutine is stopped or has completed.
        ///
        /// **Note:** You can stop a coroutine using <see cref="Stop"/>
        /// </summary>
        /// <param name="routine"> The <see cref="IEnumerator"/> routine you would like to start.</param>
        /// <returns>Started Coroutine.</returns>
        public static Coroutine Start(IEnumerator routine)
        {
            return GlobalCoroutine.Instance.StartCoroutine(routine);
        }

        /// <summary>
        /// Stops the coroutine.
        /// An equivalent for [MonoBehaviour.StopCoroutine](https://docs.unity3d.com/ScriptReference/MonoBehaviour.StopCoroutine.html)
        /// </summary>
        /// <param name="routine">The <see cref="IEnumerator"/> routine you would like to stop.</param>
        public static void Stop(IEnumerator routine)
        {
            GlobalCoroutine.Instance.StopCoroutine(routine);
        }

        /// <summary>
        /// Waits until the end of the frame after Unity has rendered every Camera and GUI, just before displaying the frame on screen.
        /// Learn more at [WaitForEndOfFrame](https://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html)
        /// </summary>
        /// <param name="action">The callback action.</param>
        public static void WaitForEndOfFrame(Action action)
        {
            GlobalCoroutine.Instance.StartInstruction(new WaitForEndOfFrame(), action);
        }

        /// <summary>
        /// Waits until next fixed frame rate update function.
        /// Learn more at [WaitForFixedUpdate](https://docs.unity3d.com/ScriptReference/WaitForFixedUpdate.html)
        /// <param name="action">The callback action.</param>
        /// </summary>
        public static void WaitForFixedUpdate(Action action)
        {
            GlobalCoroutine.Instance.StartInstruction(new WaitForFixedUpdate(), action);
        }

        /// <summary>
        ///  Wait for a given number of seconds using scaled time.
        /// Learn more at [WaitForSeconds](https://docs.unity3d.com/ScriptReference/WaitForSeconds.html)
        /// <param name="seconds">Delay execution by the amount of time in seconds.</param>
        /// <param name="action">The callback action.</param>
        /// </summary>
        public static void WaitForSeconds(float seconds, Action action)
        {
            GlobalCoroutine.Instance.StartInstruction(new WaitForSeconds(seconds), action);
        }

        /// <summary>
        /// Wait for a given number of seconds realtime.
        /// Learn more at [WaitForSecondsRealtime](https://docs.unity3d.com/ScriptReference/WaitForSecondsRealtime.html)
        /// </summary>
        /// <param name="seconds">Delay execution by the amount of time in seconds.</param>
        /// <param name="action">The callback action.</param>
        public static void WaitForSecondsRealtime(float seconds, Action action)
        {
            GlobalCoroutine.Instance.StartInstruction(new WaitForSecondsRealtime(seconds), action);
        }

        /// <summary>
        /// Wait for a random number of seconds between min _inclusive_ and max _inclusive_ interval, using scaled time.
        /// </summary>
        /// <param name="min">Min Delay of time in seconds.</param>
        /// <param name="max">Max Delay of time in seconds.</param>
        /// <param name="action">The callback action.</param>
        public static void WaitForSecondsWithRandomInterval(float min, float max, Action action)
        {
            var delay = UnityEngine.Random.Range(min, max);
            WaitForSeconds(delay, action);
        }
    }
}
