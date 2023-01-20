using UnityEngine;

namespace Sources.Logic.Extensions
{
    public static class Extensions
    {
        public static bool TryClear(this Coroutine coroutine, MonoBehaviour behaviour)
        {
            if (coroutine == null)
                return false;
            
            behaviour.StopCoroutine(coroutine);
            return true;
        }
    }
}