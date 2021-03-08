using System;
using System.Collections;
using UnityEngine;

namespace ExampleMod.Util
{
    internal static class CoroutineExtensions
    {
        internal static void DelayedMethod(float seconds, Action method)
        {
            ExampleMod.Instance.StartCoroutine(InternalDelayedMethod(seconds, method));
        }

        private static IEnumerator InternalDelayedMethod(float seconds, Action method)
        {
            yield return new WaitForSeconds(seconds);

            method();
        }
    }
}
