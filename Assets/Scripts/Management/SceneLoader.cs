using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    IEnumerator Start()
    {
        // Load the UI scene additively
        AsyncOperation async = SceneManager.LoadSceneAsync("UIBuilder", LoadSceneMode.Additive);
        yield return new WaitUntil(() => async.isDone);

      
    }
}
