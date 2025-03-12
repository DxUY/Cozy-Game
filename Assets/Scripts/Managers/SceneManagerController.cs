using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManagerController : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.OnSceneChangeRequest += LoadSceneAsync;
    }

    private void OnDisable()
    {
        EventBus.OnSceneChangeRequest -= LoadSceneAsync;
    }

    private void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        EventBus.OnSceneLoaded?.Invoke(sceneName); // 🔥 Notify AudioManager
    }
}
