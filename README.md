# ParallelCoroutine

A coroutine that can be executed in parallel.

![output-palette-none](https://user-images.githubusercontent.com/144386/135176258-921fc1f9-a6ca-49ff-8e66-6f529c2544da.gif)

## Usage

```cs
using System.Collections;
using System.Collections.Generic;
using info.shibuya24;
using UnityEngine;

public class ParallelCoroutineTest : MonoBehaviour
{
    IEnumerator Start()
    {
        var result = ParallelCoroutine.Execute(this, new List<IEnumerator>()
        {
            TestCoroutine(1),
            TestCoroutine(2, 2),
            TestCoroutine(3, 3),
            TestCoroutine(4, 4),
            TestCoroutine(5, 5)
        }, () => Debug.Log("AllComplete"));

        yield return new WaitForSeconds(2f);
        
        // Cancel Coroutine
        StopCoroutine(result.mainCoroutine);
        foreach (var col in result.childCoroutineList)
            StopCoroutine(col);
    }

    IEnumerator TestCoroutine(int id, float sec = 1f)
    {
        Debug.Log($"Start : {id}");
        yield return new WaitForSeconds(sec);
        Debug.Log($"End : {id}");
    }
}
```


## Required
  
- Unity 2018.4 or higher


## Using UPM

You can add `https://github.com/baobao/ParallelCoroutine.git` to Package Manager,
or add `"info.shibuya24.parallelcoroutine":"https://github.com/baobao/ParallelCoroutine.git"` to `Packages/manifest.json`.
  

## License

This library is under the MIT License.
