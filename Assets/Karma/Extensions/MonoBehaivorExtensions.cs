using System;
using System.Collections;
using UnityEngine;

namespace Karma.Extensions
{
    public static class MonoBehaivorExtensions
    {
        public static Coroutine StartCoroutineSafe(this MonoBehaviour mono, ref Coroutine coroutine,
            IEnumerator routine)
        {
            if (coroutine != null)
                mono.StopCoroutine(coroutine);
            return coroutine = mono.StartCoroutine(routine);
        }

        public static void DelayedAction(this MonoBehaviour mono, float delay, Action action)
        {
            mono.StartCoroutine(DelayedActionCoroutine(delay, action));
        }
        
        private static IEnumerator DelayedActionCoroutine(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action();
        }
    }
}