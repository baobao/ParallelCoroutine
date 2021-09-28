using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace info.shibuya24
{
    public static class ParallelCoroutine
    {
        public static (Coroutine mainCoroutine, List<Coroutine> childCoroutineList) 
            Execute(MonoBehaviour owner, IList<IEnumerator> ienumeratorList, Action complete = null)
        {
            List<Coroutine> childCoroutineList = null;
            var mainCoroutine = owner.StartCoroutine(InternalExecute(owner, ienumeratorList, complete, x => childCoroutineList = x));
            return (mainCoroutine, childCoroutineList);
        }

        private static IEnumerator InternalExecute(MonoBehaviour owner, 
            IList<IEnumerator> ienumeratorList, Action complete, Action<List<Coroutine>> childCoroutineList)
        {
            List<Coroutine> coroutineList = new List<Coroutine>();
            for (int i = 0; i < ienumeratorList.Count; i++)
            {
                var ienumerator = ienumeratorList[i];
                coroutineList.Add(owner.StartCoroutine(ienumerator));
            }
            childCoroutineList.Invoke(coroutineList);

            foreach (var col in coroutineList)
            {
                yield return col;
            }

            complete?.Invoke();
        }
    }
}

