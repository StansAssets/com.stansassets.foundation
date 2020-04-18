using System;
using System.Collections;
using StansAssets.Foundation.Patterns;
using UnityEngine;

namespace StansAssets.Foundation.Async
{
    class GlobalCoroutine : MonoSingleton<GlobalCoroutine>
    {
        public void StartInstruction(YieldInstruction instruction, Action action)
        {
            StartCoroutine(RunActionAfterInstruction(instruction, action));
        }

        IEnumerator RunActionAfterInstruction(YieldInstruction instruction, Action action)
        {
            yield return instruction;
            action.Invoke();
        }

        public void StartInstruction(CustomYieldInstruction instruction, Action action)
        {
            StartCoroutine(RunActionAfterInstruction(instruction, action));
        }

        IEnumerator RunActionAfterInstruction(CustomYieldInstruction instruction, Action action)
        {
            yield return instruction;
            action.Invoke();
        }
    }
}
